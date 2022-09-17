using BluePrism.TechnicalTest.Common.Exceptions;
using BluePrism.TechnicalTest.Files;
using BluePrism.TechnicalTest.Services.Files;
namespace BluePrism.TechnicalTest.Services.Tests.FileService
{
    public class FileServiceTestSaveInformation
    {
        private IFileService _fileService;
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
        public void SaveFileDataInformationNoProvidedFilePathException()
        {
            var argumentException = Assert.Throws<ArgumentInvalidException>(() => _fileService.SaveFileDataInformation(null, new List<string>() { "WriteTextListNoProvidedFilePath" }));

            Assert.That(argumentException.Message, Is.EqualTo("Filepath must be provided."));
        }
        [Test]
        public void SaveFileDataInformationNoTextToWriteException()
        {
            var argumentException = Assert.Throws<ArgumentInvalidException>(() => _fileService.SaveFileDataInformation(new FileInfo($"{_urlBluePrismFilesTestsPath}\\ResultFile.txt"), new List<string>()));

            Assert.That(argumentException.Message, Is.EqualTo("Text to Write on the File must be provided."));
        }
        [Test]
        public void SaveFileDataInformationWrongFilePathException()
        {
            var argumentException = Assert.Throws<FileWrittingException>(() => _fileService.SaveFileDataInformation(new FileInfo("sds\\d"), new List<string>() { "WriteTextListNoProvidedFilePath" }));

            Assert.That(argumentException.Message.Contains("Could not find a part of the path"), Is.True);
        }
        [Test]
        public void SaveFileDataInformationStringToWriteSuccess()
        {
            var fileUrl = $"{_urlBluePrismFilesTestsPath}\\ResultFile.txt";

            _fileService.SaveFileDataInformation(new FileInfo(fileUrl), _linesSuccess);

            IEnumerable<string> allItmes = _fileService.GetFileDataInformation(new FileInfo(fileUrl));

            Assert.Multiple(() =>
            {
                Assert.That(allItmes.GetType(), Is.EqualTo(typeof(string[])));
                Assert.That(allItmes.Count(), Is.EqualTo(_linesSuccess.Count));
                CollectionAssert.AreEqual(_linesSuccess, allItmes);
            });
        }
    }
}