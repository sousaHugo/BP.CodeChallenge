namespace BluePrism.TechnicalTest.Contracts.Interfaces.DictionaryProcessing
{
    public interface IDictionaryProcessing
    {
        IEnumerable<string> ProcessWordDictionaryValidate(string StartWord, string EndWord, HashSet<string> WordDictionary);
        IEnumerable<string> ProcessWordDictionary(string StartWord, string EndWord, HashSet<string> WordDictionary);
    }
}
