namespace BluePrism.TechnicalTest.Files
{
    public interface IFileOperation
    {
        IEnumerable<string> Get(string FilePath);
        void Create(string FilePath, string Text);
        void Create(string FilePath, IEnumerable<string> TextList);
    }
}
