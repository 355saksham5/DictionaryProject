using DictionaryApi.Models;
using FluentValidation;

namespace DictionaryApi.FluentValidators
{
    public class RegisterValidator : AbstractValidator<RegisterModel>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.Email)
                 .NotEmpty().WithMessage("Email is Required.")
                 .EmailAddress().WithMessage("Valid Email Address is Required.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is Required.")
                .Length(6, 100).WithMessage("Password must be between 6 and 100 characters.")
                .Matches(@".*[A-Z]+.*").WithMessage("Your password must contain at least one uppercase letter.")
                .Matches(@".*[a-z]+.*").WithMessage("Your password must contain at least one lowercase letter.")
                .Matches(@".*[0-9]+.*").WithMessage("Your password must contain at least one number.")
                .Matches(@".*[\!\?\*\.]+.*").WithMessage("Your password must contain at least one (!? *.).");
        }
    }
}
