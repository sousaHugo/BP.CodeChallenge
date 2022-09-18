namespace BluePrism.TechnicalTest.Contracts.Interfaces.Files
{
    public interface IFileService
    {
        /// <summary>
        /// Every class and member should have a one sentence
        /// summary describing its purpose.
        /// </summary>
        IEnumerable<string> GetFileDataInformation(FileInfo FileInfo);
        void SaveFileDataInformation(FileInfo FileInfo, IEnumerable<string> FileDataInformation);
    }
}
