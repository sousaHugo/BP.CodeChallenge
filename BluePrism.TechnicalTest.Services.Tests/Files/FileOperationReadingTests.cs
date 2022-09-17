using BluePrism.TechnicalTest.Common.Exceptions;
using BluePrism.TechnicalTest.Files;

namespace BluePrism.TechnicalTest.Tests.Files
{
    public class FileOperationReadingTests
    {
        private IFileOperation _fileOperation;
        private readonly string _correctFileName = "teste.txt";
        private readonly string _incorrectFileName = "jjeste.txt";

        private readonly List<string> _linesSuccess = new List<string>() { "Spin", "Spin", "Spit", "Spot" };

        [SetUp]
        public void Setup()
        {
            _fileOperation = new FileOperation();
        }

        [Test]
        public void ReadAllLinesNoProvidedFilePath()
        {
            var argumentException = Assert.Throws<ArgumentInvalidException>(() => _fileOperation.Get(null));

            Assert.That(argumentException.Message, Is.EqualTo("Filepath must be provided."));
        }

        [Test]
        public void ReadAllLinesFileNotFound()
        {
            var notFoundException = Assert.Throws<FilePathNotFoundException>(() => _fileOperation.Get(_incorrectFileName));

            Assert.That(notFoundException.Message, Is.EqualTo($"Filepath: {_incorrectFileName} not found."));
        }

        [Test]
        public void ReadAllLinesSuccess()
        {
            var resultList = _fileOperation.Get(_correctFileName);

            Assert.Multiple(() =>
            {
                Assert.That(resultList.Any(), Is.True);
                Assert.That(resultList, Is.EqualTo(_linesSuccess));

            });
        }

    }
}