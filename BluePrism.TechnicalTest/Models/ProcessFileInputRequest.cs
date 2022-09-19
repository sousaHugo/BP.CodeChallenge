using BluePrism.TechnicalTest.Common.Constants;
using BluePrism.TechnicalTest.Contracts.Dtos;
using FluentValidation;

namespace BluePrism.TechnicalTest.Models
{
    public record ProcessFileInputRequest(string DictionaryFileUrl, string StartWord, string EndWord, string ResultFileUrl);

    /// This class contains all the extensions methods of the record <see cref="ProcessFileInputDto"/>.
    /// <list type="bullet">
    /// <item>
    /// <term><see cref="ProcessFileInputDtoValidation.Validate(ProcessFileInputDto)"/></term>
    /// <description>Validates if the object is valid for processing.</description>
    /// </item>
    /// </list>
    /// </summary>
    public class ProcessFileInputRequestValidator : AbstractValidator<ProcessFileInputRequest>
    {
        /// <summary>
        /// This method is going to validate if the <see cref="ProcessFileInputDto"/> is valid to be processed.
        /// <para>If any error is encountered it will written on the console.</para>
        /// </summary>
        /// <param name="ProcessFileInputDto" cref="ProcessFileInputDto">Object with all the information that process needs to run.</param>
        /// <returns>
        /// This method returns a True or False. True if the object is valid and False if the object is not valid.
        /// </returns>
        public ProcessFileInputRequestValidator()
        {
            RuleFor(x => x.DictionaryFileUrl).NotNull().NotEmpty().WithMessage("Word Dictionary File is required.");
            RuleFor(x => x.StartWord).NotNull().NotEmpty().WithMessage("Start Word is required.");
            RuleFor(x => x.EndWord).NotNull().NotEmpty().WithMessage("End Word is required.");
            RuleFor(x => x.ResultFileUrl).NotNull().NotEmpty().WithMessage("Result File Name is required.");
            RuleFor(x => x.StartWord).Length(ProcessingConstants.LettersMaxLength).WithMessage("Start Word must have 4 Charachters.");
            RuleFor(x => x.EndWord).Length(ProcessingConstants.LettersMaxLength).WithMessage("End Word must have 4 Charachters.");
            RuleFor(x => x.DictionaryFileUrl).Must(fileUrl => File.Exists(fileUrl)).WithMessage("Dictionary File Path doesn't exists.");
        }
    }

}
