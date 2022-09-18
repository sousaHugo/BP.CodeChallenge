using BluePrism.TechnicalTest.Common.Exceptions;
using BluePrism.TechnicalTest.Contracts.Interfaces.Files;
using BluePrism.TechnicalTest.Files;

namespace BluePrism.TechnicalTest.Services.Files
{
    public class FileService : IFileService
    {
        private readonly IFileOperation _fileOperation;
        public FileService(IFileOperation FileOperation)
        {
            _fileOperation = FileOperation;
        }

        ///<inheritdoc cref="IFileService.GetFileDataInformation(FileInfo)"/>
        public IEnumerable<string> GetFileDataInformation(FileInfo FileInfo)
        {
            if (FileInfo == null)
                throw new ArgumentInvalidException($"Filepath must be provided.");

            return _fileOperation.Get(FileInfo.FullName);
        }

        ///<inheritdoc cref="IFileService.SaveFileDataInformation(FileInfo, IEnumerable{string})"/>
        public void SaveFileDataInformation(FileInfo FileInfo, IEnumerable<string> FileDataInformation)
        {
            if(FileInfo == null)
                throw new ArgumentInvalidException($"Filepath must be provided.");

            if (FileDataInformation == null || !FileDataInformation.Any() || FileDataInformation.Any((string a) => string.IsNullOrEmpty(a)))
                throw new ArgumentInvalidException("Text to Write on the File must be provided.");

            _fileOperation.Create(FileInfo.FullName, FileDataInformation);
        }
    }
}