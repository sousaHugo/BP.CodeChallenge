using FluentValidation;

namespace BluePrism.TechnicalTest.Contracts.Dtos
{
    public class FileGetDataInformationDto
    {
        public FileInfo File { get; set; }
    }

    /// <summary>
    /// This class contains all the validator method of the class <see cref="FileGetDataInformationDto"/>.
    /// <list type="bullet">
    /// <item>
    /// <term><see cref="FileGetDataInformationValidator.Validate(ProcessFileInputDto)"/></term>
    /// <description>Validates if the object is valid for processing.</description>
    /// </item>
    /// </list>
    /// </summary>
    public class FileGetDataInformationValidator : AbstractValidator<FileGetDataInformationDto>
    {
        /// <summary>
        /// This method is going to validate if the <see cref="FileGetDataInformationDto"/> is valid to be processed.
        /// <para>All the validator rules are specified in here</para>
        /// </summary>
        public FileGetDataInformationValidator()
        {
            RuleFor(x => x.File).NotNull().WithMessage("File must be provided.");
            RuleFor(x => x.File).Must(a => a != null && File.Exists(a.FullName)).WithMessage("File Path is not valid.");
        }
    }
}
