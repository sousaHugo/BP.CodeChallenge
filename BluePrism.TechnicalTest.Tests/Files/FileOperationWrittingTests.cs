using BluePrism.TechnicalTest.Common.Exceptions;

namespace BluePrism.TechnicalTest.Files.Tests
{
    public class FileOperationWrittingTests
    {
        private IFileOperation _fileOperation;
        private readonly string _urlBluePrismFilesTestsPath = @"C:\BluePrismFiles";
        private readonly List<string> _linesSuccess = new List<string>() { "Spin", "Spin", "Spit", "Spot" };

        [SetUp]
        public void Setup()
        {
            _fileOperation = new FileOperation();

            if (!Directory.Exists(_urlBluePrismFilesTestsPath))
                Directory.CreateDirectory(_urlBluePrismFilesTestsPath);
        }

        [Test]
        public void CreateNoProvidedFilePathException()
        {
            var argumentException = Assert.Throws<ArgumentInvalidException>(() => _fileOperation.Create(string.Empty, "CreateNoProvidedFilePath"));

            Assert.That(argumentException.Message, Is.EqualTo("Filepath must be provided."));
        }
        [Test]
        public void CreateNoTextToWriteException()
        {
            var argumentException = Assert.Throws<ArgumentInvalidException>(() => _fileOperation.Create($"{_urlBluePrismFilesTestsPath}\\ResultFile.txt", string.Empty));

            Assert.That(argumentException.Message, Is.EqualTo("Text to Write on the File must be provided."));
        }
        [Test]
        public void CreateWrongFilePathException()
        {
            var argumentException = Assert.Throws<FileWrittingException>(() => _fileOperation.Create("sds\\d", "CreateListNoProvidedFilePath"));

            Assert.That(argumentException.Message.Contains("Could not find a part of the path"), Is.True);
        }
        [Test]
        public void CreateStringToWriteSuccess()
        {
            var fileUrl = $"{_urlBluePrismFilesTestsPath}\\ResultFile.txt";
            var fileTextToWrite = "This is a Test";

            _fileOperation.Create(fileUrl, fileTextToWrite);

            var allText = _fileOperation.Get(fileUrl);

            Assert.That(allText, Is.Not.Empty);

            Assert.Multiple(() =>
            {
                Assert.That(allText.Count(), Is.EqualTo(1));
                Assert.That(allText.FirstOrDefault(), Is.Not.Null);
                Assert.That(allText.First(), Is.EqualTo(fileTextToWrite));
            });
        }

        [Test]
        public void CreateListNoProvidedFilePathException()
        {
            var argumentException = Assert.Throws<ArgumentInvalidException>(() => _fileOperation.Create(string.Empty, new List<string>() { "CreateListNoProvidedFilePath" }));

            Assert.That(argumentException.Message, Is.EqualTo("Filepath must be provided."));
        }
        [Test]
        public void CreateListNoTextToWriteException()
        {
            var argumentException = Assert.Throws<ArgumentInvalidException>(() => _fileOperation.Create($"{_urlBluePrismFilesTestsPath}\\ResultFile.txt", new List<string>()));

            Assert.That(argumentException.Message, Is.EqualTo("Text to Write on the File must be provided."));
        }
        [Test]
        public void CreateListWrongFilePathException()
        {
            var argumentException = Assert.Throws<FileWrittingException>(() => _fileOperation.Create("sds\\d", new List<string>() { "CreateListNoProvidedFilePath" }));

            Assert.That(argumentException.Message.Contains("Could not find a part of the path"), Is.True);
        }
        [Test]
        public void CreateListStringToWriteSuccess()
        {
            var fileUrl = $"{_urlBluePrismFilesTestsPath}\\ResultFile.txt";

            _fileOperation.Create(fileUrl, _linesSuccess);

            IEnumerable<string> allItmes = _fileOperation.Get(fileUrl);
            
            Assert.Multiple(() =>
            {
                Assert.That(allItmes.GetType(), Is.EqualTo(typeof(string[])));
                Assert.That(allItmes.Count(), Is.EqualTo(_linesSuccess.Count));
                CollectionAssert.AreEqual(_linesSuccess, allItmes);
            });
        }
    }
}