using BluePrism.TechnicalTest.Contracts.Dtos;
using BluePrism.TechnicalTest.Contracts.Interfaces.DictionaryProcessing;
using BluePrism.TechnicalTest.Services.Processing;
using FluentValidation.Results;
using NUnit.Framework;

namespace BluePrism.TechnicalTest.Tests.Services.DictionaryProcessing
{
    public class DictionaryProcessingValidateTest
    {
        private IDictionaryProcessing _dictionaryService;
        private readonly string _startWord = "Spin";
        private readonly string _endWord = "Spot";
        private HashSet<string> _dictionaryNames;

        [SetUp]
        public void Setup()
        {
            _dictionaryService = new DictionaryProcessingService(new ProcessFileInputValidator());

            _dictionaryNames = new HashSet<string>();
            _dictionaryNames.Add("Spin");
            _dictionaryNames.Add("Spon");
            _dictionaryNames.Add("Spot");
        }

        [Test]
        public void ProcessWordDictionaryValidateStartWordEmpty()
        {
            var validationResult = _dictionaryService.ProcessWordDictionaryValidate(new ProcessFileInputDto() { EndWord = _endWord, StartWord = string.Empty, WordsDictionary = _dictionaryNames });    

            Assert.Multiple(() =>
            {
                Assert.That(validationResult.GetType(), Is.EqualTo(typeof(ValidationResult)));
                Assert.That(validationResult.Errors.Count, Is.EqualTo(1));
                Assert.That(validationResult.Errors.FirstOrDefault(), Is.Not.Null);
                Assert.That(validationResult.Errors.First().ErrorMessage, Is.EqualTo("Start Word must have 4 Charachters."));
            });
        }
        [Test]
        public void ProcessWordDictionaryValidateEndWordEmpty()
        {
            var validationResult = _dictionaryService.ProcessWordDictionaryValidate(new ProcessFileInputDto() { EndWord = string.Empty, StartWord = _startWord, WordsDictionary = _dictionaryNames });

            Assert.That(validationResult.GetType(), Is.EqualTo(typeof(ValidationResult)));
            Assert.That(validationResult.Errors.Count, Is.EqualTo(2));
            Assert.That(validationResult.Errors.FirstOrDefault(), Is.Not.Null);
            Assert.That(validationResult.Errors.Any(a => a.ErrorMessage.Equals("End Word must have 4 Charachters.")), Is.True);
            Assert.That(validationResult.Errors.Any(a => a.ErrorMessage.Equals("The Dictionary does not contains the End Word.")), Is.True);
        }
        [Test]
        public void ProcessWordDictionaryValidateStartEndWordSame()
        {
            var validationResult = _dictionaryService.ProcessWordDictionaryValidate(new ProcessFileInputDto() { EndWord = _startWord, StartWord = _startWord, WordsDictionary = _dictionaryNames });

            Assert.Multiple(() =>
            {
                Assert.That(validationResult.GetType(), Is.EqualTo(typeof(ValidationResult)));
                Assert.That(validationResult.Errors.Count, Is.EqualTo(1));
                Assert.That(validationResult.Errors.FirstOrDefault(), Is.Not.Null);
                Assert.That(validationResult.Errors.First().ErrorMessage, Is.EqualTo("Start Word and End Word are the same."));
            });
        }
        [Test]
        public void ProcessWordDictionaryValidateEmptyDictionary()
        {
            var validationResult = _dictionaryService.ProcessWordDictionaryValidate(new ProcessFileInputDto() { EndWord = _endWord, StartWord = _startWord, WordsDictionary = new List<string>() });

            Assert.Multiple(() =>
            {
                Assert.That(validationResult.GetType(), Is.EqualTo(typeof(ValidationResult)));
                Assert.That(validationResult.Errors.Count, Is.EqualTo(3));
                Assert.That(validationResult.Errors.Any(a => a.ErrorMessage.Equals("The Word Dictionary could not be Empty.")), Is.True);
                Assert.That(validationResult.Errors.Any(a => a.ErrorMessage.Equals("The Word Dictionary must have at least two words.")), Is.True);
                Assert.That(validationResult.Errors.Any(a => a.ErrorMessage.Equals("The Dictionary does not contains the End Word.")), Is.True);
                
            });
        }
        [Test]
        public void ProcessWordDictionaryValidateOneItemDictionary()
        {
            var validationResult = _dictionaryService.ProcessWordDictionaryValidate(new ProcessFileInputDto() { EndWord = _endWord, StartWord = _startWord, WordsDictionary = new List<string>() { "Spot" }.AsEnumerable() });

            Assert.Multiple(() =>
            {
                Assert.That(validationResult.GetType(), Is.EqualTo(typeof(ValidationResult)));
                Assert.That(validationResult.Errors.Count, Is.EqualTo(1));
                Assert.That(validationResult.Errors.Any(a => a.ErrorMessage.Equals("The Word Dictionary must have at least two words.")), Is.True);

            });
        }
        [Test]
        public void ProcessWordDictionaryValidateNoEndWordDictionary()
        {
            var validationResult = _dictionaryService.ProcessWordDictionaryValidate(new ProcessFileInputDto() { EndWord = _endWord, StartWord = _startWord, WordsDictionary = new List<string>() { "Spin", "Spon", "Spat" }.AsEnumerable() });

            Assert.Multiple(() =>
            {                
                Assert.That(validationResult.GetType(), Is.EqualTo(typeof(ValidationResult)));
                Assert.That(validationResult.Errors.Count, Is.EqualTo(1));
                Assert.That(validationResult.Errors.Any(a => a.ErrorMessage.Equals("The Dictionary does not contains the End Word.")), Is.True);
            });
        }

        [Test]
        public void ProcessWordDictionaryValidateSuccessValidation()
        {
            var validationResult = _dictionaryService.ProcessWordDictionaryValidate(new ProcessFileInputDto() { EndWord = _endWord, StartWord = _startWord, WordsDictionary = new List<string>() { "Spin", "Spon", "Spot" }.AsEnumerable() });
            
            Assert.Multiple(() =>
            {
                Assert.That(validationResult.GetType(), Is.EqualTo(typeof(ValidationResult)));
                Assert.That(validationResult.Errors.Any(), Is.False);
            });
        }
        [Test]
        public void ProcessWordDictionaryValidateSuccessProcessing()
        {
            var dictionary = new List<string>() { "Spin", "Spit", "Span", "Spot", "Span" }.AsEnumerable();
            var validationResult = _dictionaryService.ProcessWordDictionaryValidate(new ProcessFileInputDto() { EndWord = _endWord, StartWord = _startWord, WordsDictionary = dictionary });
            var results = _dictionaryService.ProcessWordDictionary(new ProcessFileInputDto() { EndWord = _endWord, StartWord = _startWord, WordsDictionary = dictionary });

            Assert.Multiple(() =>
            {
                Assert.That(validationResult.GetType(), Is.EqualTo(typeof(ValidationResult)));
                Assert.That(validationResult.Errors.Any(), Is.False);

                Assert.That(results.Any(), Is.True);
                CollectionAssert.AreEqual(results, new List<string>() { "Spin", "Spit", "Spot" }.AsEnumerable());
            });
        }
    }
}