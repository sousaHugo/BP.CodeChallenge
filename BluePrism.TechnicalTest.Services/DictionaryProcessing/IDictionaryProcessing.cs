namespace BluePrism.TechnicalTest.Services.Processing
{
    public interface IDictionaryProcessing
    {
        IEnumerable<string> ProcessWordDictionaryValidate(string StartWord, string EndWord, HashSet<string> WordDictionary);
        IEnumerable<LinkedList<string>> ProcessWordDictionary(string StartWord, string EndWord, HashSet<string> WordDictionary);

        List<LinkedList<string>> findLadders(string start, string end, HashSet<string> dict);
    }
}