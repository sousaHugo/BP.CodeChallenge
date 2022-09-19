using BluePrism.TechnicalTest.Common.Constants;
using FluentValidation;

namespace BluePrism.TechnicalTest.Contracts.Dtos
{
    public class ProcessFileInputDto
    {
        public string StartWord { get; set; }
        public string EndWord { get; set; }
        public IEnumerable<string> WordsDictionary { get; set; }
    }

    /// <summary>
    /// This class contains all the validator method of the class <see cref="ProcessFileInputDto"/>.
    /// <list type="bullet">
    /// <item>
    /// <term><see cref="ProcessFileInputValidator.Validate(ProcessFileInputDto)"/></term>
    /// <description>Validates if the object is valid for processing.</description>
    /// </item>
    /// </list>
    /// </summary>
    public class ProcessFileInputValidator : AbstractValidator<ProcessFileInputDto>
    {
        /// <summary>
        /// This method is going to validate if the <see cref="ProcessFileInputDto"/> is valid to be processed.
        /// <para>All the validator rules are specified in here</para>
        /// </summary>
        public ProcessFileInputValidator()
        {
            RuleFor(x => x.StartWord).NotNull().WithMessage("Start Word is required.");
            RuleFor(x => x.EndWord).NotNull().WithMessage("End Word is required.");
            RuleFor(x => x.StartWord).Length(ProcessingConstants.LettersMaxLength).WithMessage("Start Word must have 4 Charachters."); 
            RuleFor(x => x.EndWord).Length(ProcessingConstants.LettersMaxLength).WithMessage("End Word must have 4 Charachters.");
            RuleFor(x => x.WordsDictionary).NotNull().NotEmpty().WithMessage("The Word Dictionary could not be Empty.");
            RuleFor(x => x.WordsDictionary).Must(a => a.Count() > ProcessingConstants.DictionaryMinLength).WithMessage("The Word Dictionary must have at least two words.");
            RuleFor(x => x.StartWord.ToUpper()).NotEqual(a => a.EndWord.ToUpper()).WithMessage("Start Word and End Word are the same.");
            RuleFor(x => x).Custom((a, context) =>
            {
                if (!a.WordsDictionary.Any(word => word.ToUpper().Equals(a.EndWord.ToUpper())))
                    context.AddFailure("The Dictionary does not contains the End Word.");
            });
        }
    }

}
