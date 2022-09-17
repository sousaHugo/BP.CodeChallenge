namespace BluePrism.TechnicalTest.Services.Files
{
    public interface IFileService
    {
        IEnumerable<string> GetFileDataInformation(FileInfo FileInfo);
        void SaveFileDataInformation(FileInfo FileInfo, IEnumerable<string> FileDataInformation);
    }
}