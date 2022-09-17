using System.ComponentModel;
using System.Linq;

namespace BluePrism.TechnicalTest.Services.Processing
{
    public class DictionaryProcessing : IDictionaryProcessing
    {
        public IEnumerable<string> ProcessWordDictionaryValidate(string StartWord, string EndWord, HashSet<string> WordDictionary)
        {
            if (!WordDictionary.Any())
                yield return "The Word Dictionary is Empty.";

            if(WordDictionary.Count < 2)
                yield return "The Word Dictionary must have at least two words.";

            if (string.IsNullOrEmpty(StartWord))
                yield return "Start Word is required to Process the Dictionary.";

            if (string.IsNullOrEmpty(EndWord))
                yield return "End Word is required to Process the Dictionary.";

            if (!string.IsNullOrEmpty(EndWord) && !WordDictionary.Contains(EndWord) && WordDictionary.Any() && WordDictionary.Count > 2)
                yield return "The Dictionary does not contains the End Word.";

            if (StartWord.Equals(EndWord))
                yield return "Start Word and End Word are the same.";


        }
        public IEnumerable<LinkedList<string>> ProcessWordDictionary(string StartWord, string EndWord, HashSet<string> WordDictionary)
        {

            

            StartWord = StartWord.ToLower();
            EndWord = EndWord.ToLower();

            var usedWords = new HashSet<string>();
            var notUsedWord = new HashSet<string>(WordDictionary);
            var wordQueue = new Queue<WordInfo>();
            var resultList = new List<LinkedList<string>>();
            wordQueue.Enqueue(new WordInfo(StartWord, 0));

            var previousNodeSteps = 0;

            while (wordQueue.Any())
            {
                var firstQueueItem = wordQueue.Dequeue();

                if (firstQueueItem.Word.ToLower().Equals(EndWord))
                {
                    var tempLinkedList = new LinkedList<string>();
                    tempLinkedList.AddFirst(firstQueueItem.Word);
                    while (firstQueueItem.PreviousWord != null)
                    {
                        tempLinkedList.AddFirst(firstQueueItem.PreviousWord.Word);
                        firstQueueItem = firstQueueItem.PreviousWord;
                    }

                    if(!tempLinkedList.Any(a => a.ToLower().Equals(StartWord)))
                        tempLinkedList.AddFirst(StartWord);

                    resultList.Add(tempLinkedList);
                }

                if (previousNodeSteps < firstQueueItem.TotalSteps)
                {
                    notUsedWord.ExceptWith(usedWords);
                }

                previousNodeSteps = firstQueueItem.TotalSteps;

                var w = firstQueueItem.Word.ToCharArray();
                for (var i = 0; i < w.Length; i++)
                {
                    var tempC = w[i];
                    for (var c = 'a'; c <= 'z'; c++)
                    {
                        w[i] = c;

                        if (notUsedWord.Contains(new string(w)))
                        {
                            wordQueue.Enqueue(new WordInfo(new string(w), firstQueueItem.TotalSteps + 1, firstQueueItem));
                            usedWords.Add(new string(w));
                        }
                    }

                    w[i] = tempC;
                }
            }

            return resultList;
        }

        public List<LinkedList<string>> findLadders(string start, string end, HashSet<string> dict)
        {
            var visited = new HashSet<string>();
            var unvisited = new HashSet<string>();

            var preSteps = 0; // SAY WHAT
            var q = new Queue<WordInfo>();
            var result = new List<LinkedList<string>>();

            dict.Add(end);
            foreach (var d in dict) unvisited.Add(d);

            q.Enqueue(new WordInfo(start, 0, null));
            while (q.Count > 0)
            {
                var top = q.Dequeue();

                // check if we found the end word
                if (top.Word == end)
                {
                    // minSteps is not really needed
                    //if (minSteps == 0) minSteps = top.Steps; // see start value
                    //if (minSteps == top.Steps) { // no loops
                    // unwind from last to first, placing previous one at the front of the linked list
                    var l = new LinkedList<string>();
                    l.AddFirst(top.Word);
                    while (top.PreviousWord != null)
                    {
                        l.AddFirst(top.PreviousWord.Word);
                        top = top.PreviousWord;
                    }

                    l.AddFirst(start);

                    result.Add(l);
                    //}
                }

                // remove visited before looking up new matches for the next step
                if (preSteps < top.TotalSteps)
                {
                    unvisited.ExceptWith(visited);
                    Console.WriteLine("preSteps " + preSteps);
                }

                preSteps = top.TotalSteps;


                // while not found, find the next step
                var w = top.Word.ToCharArray();
                for (var i = 0; i < w.Length; i++)
                {
                    var tempC = w[i];
                    for (var c = 'a'; c <= 'z'; c++)
                    {
                        w[i] = c;

                        if (unvisited.Contains(new string(w)))
                        {
                            q.Enqueue(new WordInfo(new string(w), top.TotalSteps + 1, top));
                            visited.Add(new string(w));
                        }
                    }

                    w[i] = tempC;
                }
            }

            return result;
        }

    }
}
