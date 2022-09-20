using BluePrism.TechnicalTest.Common.Exceptions;
using BluePrism.TechnicalTest.Contracts.Dtos;
using BluePrism.TechnicalTest.Files;
using BluePrism.TechnicalTest.Services.Dictionary;
using Microsoft.Extensions.Logging;
using Moq;
using System;

namespace BluePrism.TechnicalTest.Tests.Services.FileService
{
    public class DictionaryServiceTestGetData
    {
        private DictionaryDataService _dictionaryDataService;
        private readonly string _correctFileName = "teste.txt";
        private readonly string _incorrectFileName = "jjeste.txt";
        private readonly string _urlBluePrismFilesTestsPath = @"C:\BluePrismFiles";
        private readonly List<string> _linesSuccess = new List<string>() { "Spin", "Spin", "Spit", "Spot" };
        private ILogger<DictionaryDataService> _loggerDataService;
        private ILogger<FileOperation> _loggerFileOperation;
        [SetUp]
        public void Setup()
        {
           
            if (!Directory.Exists(_urlBluePrismFilesTestsPath))
                Directory.CreateDirectory(_urlBluePrismFilesTestsPath);

            _loggerDataService = Mock.Of<ILogger<DictionaryDataService>>();
            _loggerFileOperation = Mock.Of<ILogger<FileOperation>>();
            _dictionaryDataService = new DictionaryDataService(new FileOperation(_loggerFileOperation), new FileGetDataInformationValidator(), new FileSaveDataInformationValidator(), _loggerDataService);
        }

        [Test]
        public void GetFileDataInformationNoProvidedFilePath()
        {
            var argumentException = Assert.Throws<ArgumentInvalidException>(() => _dictionaryDataService.GetDictionaryData(new FileGetDataInformationDto()));

            Assert.Multiple(() =>
            {
                Assert.That(argumentException.Message, Is.EqualTo("Read File Validation (File must be provided.) (File Path is not valid.)"));
                Assert.That(argumentException.InnerExceptions.Count, Is.EqualTo(2));
            });
        }

        [Test]
        public void GetFileDataInformationFileNotFound()
        {
            var fileInfo = new FileInfo(_incorrectFileName);
            var argumentException = Assert.Throws<ArgumentInvalidException>(() => _dictionaryDataService.GetDictionaryData(new FileGetDataInformationDto() { File = fileInfo }));

            Assert.Multiple(() =>
            {
                Assert.That(argumentException.Message, Is.EqualTo("Read File Validation (File Path is not valid.)"));
                Assert.That(argumentException.InnerExceptions.Count, Is.EqualTo(1));
            });
        }

        [Test]
        public void GetFileDataInformationSuccess()
        {
            var resultList = _dictionaryDataService.GetDictionaryData(new FileGetDataInformationDto() { File = new FileInfo(_correctFileName) });

            Assert.Multiple(() =>
            {
                Assert.That(resultList.Any(), Is.True);
                Assert.That(resultList, Is.EqualTo(_linesSuccess));

            });
        }
    }
}