namespace BluePrism.TechnicalTest.Common.Helper
{
    public static class URLHelper
    {
        public static FileInfo Url(string FilePath, string FileName) => new FileInfo($"{Path.GetDirectoryName(FilePath)}\\{FileName}");
    }
}
