using BluePrism.TechnicalTest.Contracts.Dtos;
using BluePrism.TechnicalTest.Contracts.Interfaces.DictionaryProcessing;
using BluePrism.TechnicalTest.Services.Processing;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using Moq;

namespace BluePrism.TechnicalTest.Tests.Services.DictionaryProcessing
{
    public class DictionaryProcessingTest
    {
        private IDictionaryProcessing _dictionaryService;
        private readonly string _startWord = "Spin";
        private readonly string _endWord = "Spot";

        [SetUp]
        public void Setup()
        {
            _dictionaryService = new DictionaryProcessingService(new ProcessFileInputValidator(), Mock.Of<ILogger<DictionaryProcessingService>>());
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