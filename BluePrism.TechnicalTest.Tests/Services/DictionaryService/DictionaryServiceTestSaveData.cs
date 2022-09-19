using BluePrism.TechnicalTest.Common.Exceptions;
using BluePrism.TechnicalTest.Contracts.Dtos;
using BluePrism.TechnicalTest.Contracts.Interfaces.Dictionary;
using BluePrism.TechnicalTest.Files;
using BluePrism.TechnicalTest.Services.Dictionary;

namespace BluePrism.TechnicalTest.Tests.Services.FileService
{
    public class DictionaryServiceTestSaveData
    {
        private DictionaryDataService _dictionaryDataService;
        private readonly string _urlBluePrismFilesTestsPath = @"C:\BluePrismFiles";
        private readonly List<string> _linesSuccess = new List<string>() { "Spin", "Spin", "Spit", "Spot" };

        [SetUp]
        public void Setup()
        {
            _dictionaryDataService = new DictionaryDataService(new FileOperation(), new FileGetDataInformationValidator(), new FileSaveDataInformationValidator());
            if (!Directory.Exists(_urlBluePrismFilesTestsPath))
                Directory.CreateDirectory(_urlBluePrismFilesTestsPath);
        }
        [Test]
        public void SaveFileDataInformationNoProvidedFilePathException()
        {
            var argumentException = Assert.Throws<ArgumentInvalidException>(() => _dictionaryDataService.SaveDictionaryResultData(new FileSaveDataInformationDto() { File = null, DataInformation = new List<string>() { "WriteTextListNoProvidedFilePath" } }));

            Assert.Multiple(() =>
            {
                Assert.That(argumentException.Message, Is.EqualTo("Save File Validation (File must be provided.)"));
                Assert.That(argumentException.InnerExceptions.Count, Is.EqualTo(1));
            });
        }
        [Test]
        public void SaveFileDataInformationNoTextToWriteException()
        {
            var argumentException = Assert.Throws<ArgumentInvalidException>(() => _dictionaryDataService.SaveDictionaryResultData(new FileSaveDataInformationDto() { File = new FileInfo($"{_urlBluePrismFilesTestsPath}\\ResultFile.txt"), DataInformation = new List<string>() }));
            
            Assert.Multiple(() =>
            {
                Assert.That(argumentException.Message, Is.EqualTo("Save File Validation (Data Information must not be empty.)"));
                Assert.That(argumentException.InnerExceptions.Count, Is.EqualTo(1));
            });
        }
        [Test]
        public void SaveFileDataInformationWrongFilePathException()
        {
            var argumentException = Assert.Throws<FileWrittingException>(() => _dictionaryDataService.SaveDictionaryResultData(new FileSaveDataInformationDto() { File = new FileInfo("sds\\d"), DataInformation = new List<string>() { "WriteTextListNoProvidedFilePath" } }));

            Assert.That(argumentException.Message.Contains("Could not find a part of the path"), Is.True);
        }
        [Test]
        public void SaveFileDataInformationStringToWriteSuccess()
        {
            var fileUrl = $"{_urlBluePrismFilesTestsPath}\\ResultFile.txt";

            _dictionaryDataService.SaveDictionaryResultData(new FileSaveDataInformationDto() { File = new FileInfo(fileUrl), DataInformation = _linesSuccess });

            IEnumerable<string> allItmes = _dictionaryDataService.GetDictionaryData(new FileGetDataInformationDto() { File = new FileInfo(fileUrl) });

            Assert.Multiple(() =>
            {
                Assert.That(allItmes.GetType(), Is.EqualTo(typeof(string[])));
                Assert.That(allItmes.Count(), Is.EqualTo(_linesSuccess.Count));
                CollectionAssert.AreEqual(_linesSuccess, allItmes);
            });
        }
    }
}