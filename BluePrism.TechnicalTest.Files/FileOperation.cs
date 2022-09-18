using BluePrism.TechnicalTest.Common.Exceptions;

namespace BluePrism.TechnicalTest.Files
{
    public class FileOperation : IFileOperation
    {
        public FileOperation() { }

        ///<inheritdoc cref="IFileOperation.Get(string)"/>
        public IEnumerable<string> Get(string FilePath)
        {
            if (string.IsNullOrEmpty(FilePath))
                throw new ArgumentInvalidException($"Filepath must be provided.");

            if (!File.Exists(FilePath))
                throw new FilePathNotFoundException($"Filepath: {FilePath} not found.");
            try
            {
                return File.ReadAllLines(FilePath);
            }
            catch (Exception ex)
            {
                throw new FileReadingException(ex.Message);
            }
        }
        ///<inheritdoc cref="IFileOperation.Create(string, string)"/>
        public void Create(string FilePath, string Text)
        {
            Create(FilePath, new List<string> { Text }.AsEnumerable());
        }
        
        ///<inheritdoc cref="IFileOperation.Create(string, IEnumerable{string})"/>
        public void Create(string FilePath, IEnumerable<string> TextList)
        {
            if (string.IsNullOrEmpty(FilePath))
                throw new ArgumentInvalidException($"Filepath must be provided.");

            if (TextList == null || !TextList.Any() || TextList.Any(a => string.IsNullOrEmpty(a)))
                throw new ArgumentInvalidException($"Text to Write on the File must be provided.");

            try
            {
                File.WriteAllLines(FilePath, TextList);

            }
            catch (Exception ex)
            {
                throw new FileWrittingException(ex.Message);
            }
        }
    }
}
