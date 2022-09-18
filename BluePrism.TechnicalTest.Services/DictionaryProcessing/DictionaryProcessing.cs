using BluePrism.TechnicalTest.Common.Constants;
using BluePrism.TechnicalTest.Contracts.Interfaces.DictionaryProcessing;
using System.Text;

namespace BluePrism.TechnicalTest.Services.Processing
{
    public class DictionaryProcessing : IDictionaryProcessing
    {
        ///<inheritdoc cref="IDictionaryProcessing.ProcessWordDictionaryValidate(string, string, HashSet{string})"/>
        public IEnumerable<string> ProcessWordDictionaryValidate(string StartWord, string EndWord, HashSet<string> WordDictionary)
        {
            if (!WordDictionary.Any())
                yield return "The Word Dictionary is Empty.";

            if(WordDictionary.Count < ProcessingConstants.DictionaryMinLength)
                yield return "The Word Dictionary must have at least two words.";

            if (string.IsNullOrEmpty(StartWord))
                yield return "Start Word is required to Process the Dictionary.";

            if (string.IsNullOrEmpty(EndWord))
                yield return "End Word is required to Process the Dictionary.";

            if (!string.IsNullOrEmpty(EndWord) && !WordDictionary.Any(a => a.ToUpper().Equals(EndWord.ToUpper())) 
                && WordDictionary.Any() && WordDictionary.Count > ProcessingConstants.DictionaryMinLength)
                yield return "The Dictionary does not contains the End Word.";

            if (StartWord.ToUpper().Equals(EndWord.ToUpper()))
                yield return "Start Word and End Word are the same.";

            if (!string.IsNullOrEmpty(StartWord) && StartWord.Length != ProcessingConstants.LettersMaxLength)
                yield return "Start Word must have 4 Charachters.";

            if (!string.IsNullOrEmpty(EndWord) && EndWord.Length != ProcessingConstants.LettersMaxLength)
                yield return "End Word must have 4 Charachters.";

        }

        ///<inheritdoc cref="IDictionaryProcessing.ProcessWordDictionary(string, string, HashSet{string})"/>
        public IEnumerable<string> ProcessWordDictionary(string StartWord, string EndWord, HashSet<string> WordDictionary)
        {
            Queue<string> pathQueue = new Queue<string>();
            pathQueue.Enqueue(StartWord);

            var usedWord = new List<string>() { StartWord };

            var resultList = new List<string>();

            while (pathQueue.Any())
            {
                int pathQueueCount = pathQueue.Count;
                var currentQueueWord = string.Empty;

                for (int i = 0; i < pathQueueCount; i++)
                {
                    currentQueueWord = pathQueue.Dequeue();
                    if (currentQueueWord.ToUpper().Equals(EndWord.ToUpper()))
                    {
                        resultList.Add(char.ToUpper(EndWord[0]) + EndWord.Substring(1));
                        return resultList;
                    }

                    StringBuilder currentWordStringBuilder = new StringBuilder(currentQueueWord);
                    for (int j = 0; j < StartWord.Length; j++)
                    {
                        for (int k = 0; k < 26; k++)
                        {
                            var oldChar = currentWordStringBuilder[j];
                            currentWordStringBuilder[j] = (char)('a' + k);
                            
                            var currentWordChanged = currentWordStringBuilder.ToString();

                            if (!usedWord.Any(a => a.ToUpper().Equals(currentWordChanged.ToUpper())) 
                                && WordDictionary.Any(a => a.ToUpper().Equals(currentWordChanged.ToUpper())))
                            {
                                pathQueue.Enqueue(currentWordChanged);
                                usedWord.Add(currentWordChanged.ToUpper());
                            }

                            currentWordStringBuilder[j] = oldChar;
                        }
                    }
                }
                if(!string.IsNullOrEmpty(currentQueueWord))
                    resultList.Add(char.ToUpper(currentQueueWord[0]) + currentQueueWord.Substring(1));
            }

            return resultList.AsEnumerable();
        }
    }
}
