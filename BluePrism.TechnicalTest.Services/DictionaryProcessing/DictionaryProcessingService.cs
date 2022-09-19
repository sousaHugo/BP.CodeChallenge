using BluePrism.TechnicalTest.Common.Constants;
using BluePrism.TechnicalTest.Contracts.Dtos;
using BluePrism.TechnicalTest.Contracts.Interfaces.DictionaryProcessing;
using FluentValidation;
using FluentValidation.Results;

namespace BluePrism.TechnicalTest.Services.Processing
{
    public class DictionaryProcessingService : IDictionaryProcessing
    {
        private readonly IValidator<ProcessFileInputDto> _validator;

        public DictionaryProcessingService(IValidator<ProcessFileInputDto> Validator)
        {
            _validator = Validator;
        }

        ///<inheritdoc cref="IDictionaryProcessing.ProcessWordDictionaryValidate(ProcessFileInputDto)"/>
        public ValidationResult ProcessWordDictionaryValidate(ProcessFileInputDto ProcessFileInputDto)
        {
            return _validator.Validate(ProcessFileInputDto);
        }

        ///<inheritdoc cref="IDictionaryProcessing.ProcessWordDictionary(ProcessFileInputDto)"/>
        public IEnumerable<string> ProcessWordDictionary(ProcessFileInputDto ProcessFileInputDto)
        {
            //List<IList<string>> res = new List<IList<string>>();
            List<string> res = new List<string>();

            //Removes from the processing all the Words that have different sizes from the Start and EndWord
            var dictionaryToProcess = ProcessFileInputDto.WordsDictionary.Where(a => a.Length == ProcessingConstants.LettersMaxLength).ToHashSet();

            //The queue is initialized with the Start Wword
            Queue<List<string>> pathQueue = new Queue<List<string>>();
            pathQueue.Enqueue(new List<string>() { ProcessFileInputDto.StartWord });

            List<string> checkedWords = new List<string>();
            bool pathFounded = false;

            while (pathQueue.Count > 0)
            {
                int queueSize = pathQueue.Count;

                for (int i = 0; i < queueSize; i++)
                {
                    var currPathList = pathQueue.Dequeue();

                    var curr = currPathList[currPathList.Count - 1];

                    var arr = curr.ToCharArray();
                    for (int j = 0; j < arr.Length; j++)
                    {
                        char originalChar = arr[j];
                        for (char c = 'a'; c <= 'z'; c++)
                        {
                            arr[j] = c;
                            string next = new string(arr);
                            if (dictionaryToProcess.Contains(next))
                            {
                                checkedWords.Add(next);
                                currPathList.Add(next);

                                if (next.ToUpper().Equals(ProcessFileInputDto.EndWord.ToUpper()))
                                {
                                    pathFounded = true;
                                    //res.Add(new List<string>(currPathList));
                                    res = new List<string>(currPathList);
                                }

                                pathQueue.Enqueue(new List<string>(currPathList));
                                currPathList.RemoveAt(currPathList.Count - 1);
                            }
                        }
                        arr[j] = originalChar;
                    }
                }
                
                //If a word was already checked we can remove it from the dictionary
                //with this we can try to ensure that we find the shortest path
                foreach (string str in checkedWords)
                    dictionaryToProcess.Remove(str);

                //if the shortest path is founded we can stop the process
                if (pathFounded)
                    break;
            }


            return res.DistinctBy(a => a.ToUpper());
        }
    }
}
