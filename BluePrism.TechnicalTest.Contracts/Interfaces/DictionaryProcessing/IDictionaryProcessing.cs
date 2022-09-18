namespace BluePrism.TechnicalTest.Contracts.Interfaces.DictionaryProcessing
{
    public interface IDictionaryProcessing
    {
        /// <summary>
        /// This method is going to validate if the Start Word, the End Word and the Word Dictionary are valid.
        /// </summary>
        /// <param name="StartWord">Start Word</param>
        /// <param name="EndWord">End Word</param>
        /// <param name="WordDictionary">List of Words</param>
        /// <returns>
        /// This method returns a list of string with error messages if any is found. If not the list will be returned empty.
        /// </returns>
        IEnumerable<string> ProcessWordDictionaryValidate(string StartWord, string EndWord, HashSet<string> WordDictionary);
        
        /// <summary>
        /// This method is going to process the Start Word and EndWord against the WordDictionary. The processing is goint to find
        /// the shortest path between the Start and EndWord.
        /// </summary>
        /// <param name="StartWord">Start Word</param>
        /// <param name="EndWord">End Word</param>
        /// <param name="WordDictionary">List of Words</param>
        /// <returns>
        /// This method returns the list of words between the Start and End Word.
        /// </returns>
        IEnumerable<string> ProcessWordDictionary(string StartWord, string EndWord, HashSet<string> WordDictionary);
    }
}
