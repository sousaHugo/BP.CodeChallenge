using BluePrism.TechnicalTest.Common.Exceptions;
using BluePrism.TechnicalTest.Files;
using BluePrism.TechnicalTest.Services.Files;
namespace BluePrism.TechnicalTest.Services.Tests.FileService
{
    public class FileServiceGetInformationTest
    {
        private IFileService _fileService;
        private readonly string _correctFileName = "teste.txt";
        private readonly string _incorrectFileName = "jjeste.txt";
        private readonly string _urlBluePrismFilesTestsPath = @"C:\BluePrismFiles";
        private readonly List<string> _linesSuccess = new List<string>() { "Spin", "Spin", "Spit", "Spot" };

        [SetUp]
        public void Setup()
        {
            _fileService = new BluePrism.TechnicalTest.Services.Files.FileService(new FileOperation());
            if (!Directory.Exists(_urlBluePrismFilesTestsPath))
                Directory.CreateDirectory(_urlBluePrismFilesTestsPath);
        }

        [Test]
        public void GetFileDataInformationNoProvidedFilePath()
        {
            var argumentException = Assert.Throws<ArgumentInvalidException>(() => _fileService.GetFileDataInformation(null));

            Assert.That(argumentException.Message, Is.EqualTo("Filepath must be provided."));
        }

        [Test]
        public void GetFileDataInformationFileNotFound()
        {
            var fileInfo = new FileInfo(_incorrectFileName);
            var notFoundException = Assert.Throws<FilePathNotFoundException>(() => _fileService.GetFileDataInformation(fileInfo));

            Assert.That(notFoundException.Message, Is.EqualTo($"Filepath: {fileInfo.FullName} not found."));
        }

        [Test]
        public void GetFileDataInformationSuccess()
        {
            var resultList = _fileService.GetFileDataInformation(new FileInfo(_correctFileName));

            Assert.Multiple(() =>
            {
                Assert.That(resultList.Any(), Is.True);
                Assert.That(resultList, Is.EqualTo(_linesSuccess));

            });
        }
    }
}