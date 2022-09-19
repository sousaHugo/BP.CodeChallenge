using BluePrism.TechnicalTest.Contracts.Dtos;
using FluentValidation.Results;

namespace BluePrism.TechnicalTest.Contracts.Interfaces.DictionaryProcessing
{
    public interface IDictionaryProcessing
    {
        /// <summary>
        /// This method is going to validate if the Start Word, the End Word and the Word Dictionary are valid.
        /// </summary>
        /// <param name="ProcessFileInputDto">This object contains the Start Word, End Word and Words Dictionary.</param>
        /// <returns>
        /// This method returns a list of string with error messages if any is found. This is represented on ValidationResult object.
        /// </returns>
        ValidationResult ProcessWordDictionaryValidate(ProcessFileInputDto ProcessFileInputDto);

        /// <summary>
        /// This method is going to process the Start Word and EndWord against the WordDictionary. The processing is goint to find
        /// the shortest path between the Start and EndWord.
        /// </summary>
        /// <param name="ProcessFileInputDto">This object contains the Start Word, End Word and Words Dictionary.</param>
        /// <returns>
        /// This method returns the list of words between the Start and End Word.
        /// </returns>
        IEnumerable<string> ProcessWordDictionary(ProcessFileInputDto ProcessFileInputDto);
    }
}
