namespace BluePrism.TechnicalTest.Contracts.Interfaces.Files
{
    public interface IFileService
    {
        /// <summary>
        /// This method returns all the data from a specified File.
        /// </summary>
        /// <param name="FileInfo">File to read and to get the information from.</param>
        /// <exception cref="BluePrism.TechnicalTest.Common.Exceptions.ArgumentInvalidException">
        /// Thrown when the parameter FileInfo is not valid.
        /// </exception>
        /// <exception cref="BluePrism.TechnicalTest.Common.Exceptions.FilePathNotFoundException">
        /// Thrown when the file based on the FileInfo parameter is not found.
        /// </exception>
        /// <exception cref="BluePrism.TechnicalTest.Common.Exceptions.FileReadingException">
        /// Thrown when an unexpected error occurs.
        /// </exception>
        /// <returns>
        /// This method returns a List of strings. Each item represents a line of the file.
        /// </returns>
        IEnumerable<string> GetFileDataInformation(FileInfo FileInfo);

        /// <summary>
        /// This method is going to create a new file based on the FileInfo and with the content of the parameter Text.
        /// The parameter Text is a list of strings. Each string will be saved on a new line in file. 
        /// </summary>
        /// <param name="FileInfo">File to be saved.</param>
        /// <param name="FileDataInformation">List of strings that will be saved on the file.</param>
        /// <exception cref="BluePrism.TechnicalTest.Common.Exceptions.ArgumentInvalidException">
        /// Thrown when the parameter FilePath is not filled in or if the parameter Text is null or empty.
        /// </exception>
        /// <exception cref="BluePrism.TechnicalTest.Common.Exceptions.FileReadingException">
        /// Thrown when an unexpected error occurs.
        /// </exception>
        void SaveFileDataInformation(FileInfo FileInfo, IEnumerable<string> FileDataInformation);
    }
}
