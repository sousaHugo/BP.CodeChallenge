using BluePrism.TechnicalTest.Files;
using Microsoft.Extensions.Logging;
using Moq;

namespace BluePrism.TechnicalTest.Tests.Application
{
    public class ApplicationTests
    {
        private readonly string _urlBluePrismFilesTestsPath = @"C:\BluePrismFiles";
        private readonly string _fileName = @"TesteMain.txt";
        private readonly List<string> _linesSuccess = new List<string>() { "Spin", "Spit", "Spat", "Spot", "Span" };
        private IFileOperation _fileOperation;
        private string _fullFileName = string.Empty;
        private string _outputName = "OutputFile.txt";
        private string _outputFullName = string.Empty;
        private ILogger<FileOperation> _logger;

        [SetUp]
        public void Setup()
        {
            if (!Directory.Exists(_urlBluePrismFilesTestsPath))
                Directory.CreateDirectory(_urlBluePrismFilesTestsPath);

            _logger = Mock.Of<ILogger<FileOperation>>();
            _fileOperation = new FileOperation(_logger);
            _fullFileName = $"{_urlBluePrismFilesTestsPath}\\{_fileName}";
            _outputFullName = $"{_urlBluePrismFilesTestsPath}\\{_outputName}";
            _fileOperation.Create(_fullFileName, _linesSuccess);
        }

        [Test]
        public void ApplicationSuccess()
        {
            var expectedResult = new List<string>() { "Spin", "Spit", "Spot" }.AsEnumerable();

            Program.Main(new string[4] { _fullFileName, "Spin", "Spot", _outputName });
            Assert.That(File.Exists(_outputFullName), Is.True);

            var resultData = _fileOperation.Get(_outputFullName);

            CollectionAssert.AreEqual(expectedResult, resultData);

            File.Delete(_outputFullName);
            File.Delete(_fullFileName);
        }
        [Test]
        public void ApplicationSuccessWithResultFileNameExtension()
        {
            var expectedResult = new List<string>() { "Spin", "Spit", "Spot" }.AsEnumerable();
            var outputFileName = $"{_urlBluePrismFilesTestsPath}\\OutPutFileWithExtention.txt";

            Program.Main(new string[4] { _fullFileName, "Spin", "Spot", "OutPutFileWithExtention.txt" });

            Assert.That(File.Exists(outputFileName), Is.True);

            var resultData = _fileOperation.Get(outputFileName);

            CollectionAssert.AreEqual(expectedResult, resultData);

            File.Delete(outputFileName);
            File.Delete(_fullFileName);
        }
        [Test]
        public void ApplicationSuccessWithoutResultFileNameExtension()
        {
            var expectedResult = new List<string>() { "Spin", "Spit", "Spot" }.AsEnumerable();
            var outputFileName = $"{_urlBluePrismFilesTestsPath}\\OutPutFileWithExtention.txt";

            Program.Main(new string[4] { _fullFileName, "Spin", "Spot", "OutPutFileWithExtention" });

            Assert.That(File.Exists(outputFileName), Is.True);

            var resultData = _fileOperation.Get(outputFileName);

            CollectionAssert.AreEqual(expectedResult, resultData);

            File.Delete($"{outputFileName}.txt");
            File.Delete(_fullFileName);
        }
        [Test]
        public void ApplicationEmptyDictionaryUrl()
        {
            Program.Main(new string[4] { String.Empty, "Spin", "Spot", _outputName });

            Assert.That(!File.Exists(_outputFullName), Is.True);
        }
        [Test]
        public void ApplicationEmptyStartWord()
        {
            Program.Main(new string[4] { _fullFileName, string.Empty, "Spot", _outputName });

            Assert.That(!File.Exists(_outputFullName), Is.True);
        }
        [Test]
        public void ApplicationEmptyEndWord()
        {
            Program.Main(new string[4] { _fullFileName, "Spin", string.Empty, _outputName });

            Assert.That(!File.Exists(_outputFullName), Is.True);
        }
        [Test]
        public void ApplicationEmptyResultFileName()
        {
            Program.Main(new string[4] { _fullFileName, "Spin", "Spot", string.Empty });

            Assert.That(!File.Exists(_outputFullName), Is.True);
        }
    }
}