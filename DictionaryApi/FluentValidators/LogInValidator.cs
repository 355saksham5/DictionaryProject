using DictionaryApi.Models;
using FluentValidation;

namespace DictionaryApi.FluentValidators
{
    public class LogInValidator  : AbstractValidator<LoginModel>
    {
        public LogInValidator()
        {
           RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is Required.")
                .EmailAddress().WithMessage("Valid Email Address is Required.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is Required.");
        }
    }
}
