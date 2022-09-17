using BluePrism.TechnicalTest.Contracts.Interfaces.DictionaryProcessing;

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
            _dictionaryService = new BluePrism.TechnicalTest.Services.Processing.DictionaryProcessing();
            _dictionaryNames = new HashSet<string>();
            _dictionaryNames.Add("Spin");
            _dictionaryNames.Add("Spon");
            _dictionaryNames.Add("Spot");
        }

        [Test]
        public void ProcessWordDictionaryValidateStartWordEmpty()
        {
            var validationResult = _dictionaryService.ProcessWordDictionaryValidate(string.Empty, _endWord, _dictionaryNames);
            
            Assert.Multiple(() =>
            {
                Assert.That(validationResult.Count(), Is.EqualTo(1));
                Assert.That(validationResult.First(), Is.EqualTo("Start Word is required to Process the Dictionary."));
            });
        }
        [Test]
        public void ProcessWordDictionaryValidateEndWordEmpty()
        {
            var validationResult = _dictionaryService.ProcessWordDictionaryValidate(_startWord, string.Empty, _dictionaryNames);

            Assert.Multiple(() =>
            {
                Assert.That(validationResult.Count(), Is.EqualTo(1));
                Assert.That(validationResult.First(), Is.EqualTo("End Word is required to Process the Dictionary."));
            });
        }
        [Test]
        public void ProcessWordDictionaryValidateStartEndWordSame()
        {
            var validationResult = _dictionaryService.ProcessWordDictionaryValidate(_startWord, _startWord, _dictionaryNames);

            Assert.Multiple(() =>
            {
                Assert.That(validationResult.Count(), Is.EqualTo(1));
                Assert.That(validationResult.First(), Is.EqualTo("Start Word and End Word are the same."));
            });
        }
        [Test]
        public void ProcessWordDictionaryValidateEmptyDictionary()
        {
            var validationResult = _dictionaryService.ProcessWordDictionaryValidate(_startWord, _endWord, new HashSet<string>());

            Assert.Multiple(() =>
            {
                Assert.That(validationResult.Count(), Is.EqualTo(2));
                Assert.That(validationResult.Any(a => a.Equals("The Word Dictionary is Empty.")), Is.True);
                Assert.That(validationResult.Any(a => a.Equals("The Word Dictionary must have at least two words.")), Is.True);
            });
        }
        [Test]
        public void ProcessWordDictionaryValidateOneItemDictionary()
        {
            var validationResult = _dictionaryService.ProcessWordDictionaryValidate(_startWord, _endWord, new HashSet<string>() { "fdf" });

            Assert.Multiple(() =>
            {
                Assert.That(validationResult.Count(), Is.EqualTo(1));
                Assert.That(validationResult.Any(a => a.Equals("The Word Dictionary must have at least two words.")), Is.True);
            });
        }
        [Test]
        public void ProcessWordDictionaryValidateNoEndWordDictionary()
        {
            var validationResult = _dictionaryService.ProcessWordDictionaryValidate(_startWord, _endWord, new HashSet<string>() { "Spin", "Spon", "Spat" });

            Assert.Multiple(() =>
            {
                Assert.That(validationResult.Count(), Is.EqualTo(1));
                Assert.That(validationResult.Any(a => a.Equals("The Dictionary does not contains the End Word.")), Is.True);
            });
        }

        [Test]
        public void ProcessWordDictionaryValidateSuccessValidation()
        {
            var validationResult = _dictionaryService.ProcessWordDictionaryValidate(_startWord, _endWord, new HashSet<string>() { "Spin", "Spon", "Spot" });

            Assert.That(validationResult.Any(), Is.False);
        }
    }
}