using DictionaryApp.ViewModels;
using FluentValidation;

namespace DictionaryApp.FluentValidators
{
    namespace DictionaryApp.FluentValidators
    {
        public class LogInValidator : AbstractValidator<LogInViewModel>
        {
            public LogInValidator()
            {
                RuleFor(x => x.Email)
                     .NotEmpty().WithMessage("{PropertyName} is Required.")
                     .EmailAddress().WithMessage("Valid Email Address is Required.");
                RuleFor(x => x.Password)
                    .NotEmpty().WithMessage("Password is Required.");
            }
        }
    }

}
