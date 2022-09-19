namespace BluePrism.TechnicalTest.Files
{
    public interface IFileOperation
    {
        /// <summary>
        /// This method is going to read the file that is passed for parameter.
        /// </summary>
        /// <param name="FilePath">Url where the file to read is located.</param>
        /// <exception cref="BluePrism.TechnicalTest.Common.Exceptions.ArgumentInvalidException">
        /// Thrown when the parameter FilePath is not filled in.
        /// </exception>
        /// <exception cref="BluePrism.TechnicalTest.Common.Exceptions.FilePathNotFoundException">
        /// Thrown when the file based on the FilePath parameter is not found.
        /// </exception>
        /// <exception cref="BluePrism.TechnicalTest.Common.Exceptions.FileReadingException">
        /// Thrown when an unexpected error occurs.
        /// </exception>
        /// <returns>
        /// This method returns a List of strings. Each item represents a line of the file.
        /// </returns>
        IEnumerable<string> Get(string FilePath);

        /// <summary>
        /// This method is going to create a new file based on the FilePath and with the content of the parameter Text.
        /// The parameter Text is converted to one item list and the method <see cref="Create(string, IEnumerable"/> is invoked. 
        /// </summary>
        /// <param name="FilePath">Url where the file is going to be saved.</param>
        /// <param name="Text">Text that will be saved on the file.</param>
        /// <exception cref="BluePrism.TechnicalTest.Common.Exceptions.ArgumentInvalidException">
        /// Thrown when the parameter FilePath is not filled in or if the parameter Text is null or empty.
        /// </exception>
        /// <exception cref="BluePrism.TechnicalTest.Common.Exceptions.FileReadingException">
        /// Thrown when an unexpected error occurs.
        /// </exception>
        void Create(string FilePath, string Text);
        
        /// <summary>
        /// This method is going to create a new file based on the FilePath and with the content of the parameter Text.
        /// The parameter Text is a list of strings. Each string will be saved on a new line in file. 
        /// </summary>
        /// <param name="FilePath">Url where the file is going to be saved.</param>
        /// <param name="TextList">List of strings that will be saved on the file.</param>
        /// <exception cref="BluePrism.TechnicalTest.Common.Exceptions.ArgumentInvalidException">
        /// Thrown when the parameter FilePath is not filled in or if the parameter Text is null or empty.
        /// </exception>
        /// <exception cref="BluePrism.TechnicalTest.Common.Exceptions.FileWrittingException">
        /// Thrown when an unexpected error occurs.
        /// </exception>
        void Create(string FilePath, IEnumerable<string> TextList);
    }
}
