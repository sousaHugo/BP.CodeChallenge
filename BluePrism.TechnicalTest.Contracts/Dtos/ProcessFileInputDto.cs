using BluePrism.TechnicalTest.Common.Constants;

namespace BluePrism.TechnicalTest.Contracts.Dtos
{
    public record ProcessFileInputDto(string DictionaryFileUrl, string StartWord, string EndWord, string ResultFileUrl);

    /// <summary>
    /// This class contains all the extensions methods of the record <see cref="ProcessFileInputDto"/>.
    /// <list type="bullet">
    /// <item>
    /// <term><see cref="ProcessFileInputDtoValidation.Validate(ProcessFileInputDto)"/></term>
    /// <description>Validates if the object is valid for processing.</description>
    /// </item>
    /// </list>
    /// </summary>
    public static class ProcessFileInputDtoValidation
    {
        /// <summary>
        /// This method is going to validate if the <see cref="ProcessFileInputDto"/> is valid to be processed.
        /// <para>If any error is encountered it will written on the console.</para>
        /// </summary>
        /// <param name="ProcessFileInputDto" cref="ProcessFileInputDto">Object with all the information that process needs to run.</param>
        /// <returns>
        /// This method returns a True or False. True if the object is valid and False if the object is not valid.
        /// </returns>
        public static bool Validate(this ProcessFileInputDto ProcessFileInputDto)
        {
            var errorList = new List<string>();

            if (string.IsNullOrEmpty(ProcessFileInputDto.DictionaryFileUrl))
                errorList.Add("Dictionary File Path is required");

            if (string.IsNullOrEmpty(ProcessFileInputDto.StartWord))
                errorList.Add("Start Word is required");

            if (string.IsNullOrEmpty(ProcessFileInputDto.EndWord))
                errorList.Add("End Word is required");

            if (string.IsNullOrEmpty(ProcessFileInputDto.ResultFileUrl))
                errorList.Add("Result File Name is required");

            if (!string.IsNullOrEmpty(ProcessFileInputDto.StartWord) && ProcessFileInputDto.StartWord.Length != ProcessingConstants.LettersMaxLength)
                errorList.Add("Start Word must have 4 Charachters.");

            if (!string.IsNullOrEmpty(ProcessFileInputDto.EndWord) && ProcessFileInputDto.EndWord.Length != ProcessingConstants.LettersMaxLength)
                errorList.Add("End Word must have 4 Charachters.");

            if (!File.Exists(ProcessFileInputDto.DictionaryFileUrl))
                errorList.Add("Dictionary File Path doesn't exists.");

            if (errorList.Any())
            {
                Console.WriteLine("User Inputs Are Not Valid:");
                Console.WriteLine("Errors:");

                foreach (var item in errorList) Console.WriteLine(item);

                return false;
            }
            return true;
        }
    }
}
