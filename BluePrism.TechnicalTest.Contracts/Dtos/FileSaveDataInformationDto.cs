using FluentValidation;

namespace BluePrism.TechnicalTest.Contracts.Dtos
{
    public class FileSaveDataInformationDto
    {
        public FileInfo File { get; set; }

        public IEnumerable<string> DataInformation { get; set; }
    }

    /// <summary>
    /// This class contains all the validator method of the class <see cref="FileSaveDataInformationDto"/>.
    /// <list type="bullet">
    /// <item>
    /// <term><see cref="FileSaveDataInformationValidator.Validate(ProcessFileInputDto)"/></term>
    /// <description>Validates if the object is valid for processing.</description>
    /// </item>
    /// </list>
    /// </summary>
    public class FileSaveDataInformationValidator : AbstractValidator<FileSaveDataInformationDto>
    {
        /// <summary>
        /// This method is going to validate if the <see cref="FileSaveDataInformationDto"/> is valid to be processed.
        /// <para>All the validator rules are specified in here</para>
        /// </summary>
        public FileSaveDataInformationValidator()
        {
            RuleFor(x => x.File).NotNull().WithMessage("File must be provided.");
            RuleFor(x => x.DataInformation).NotNull().NotEmpty().WithMessage("Data Information must not be empty.");
        }
    }
}
