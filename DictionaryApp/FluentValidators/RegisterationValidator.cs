using DictionaryApp.ViewModels;
using FluentValidation;

namespace DictionaryApp.FluentValidators
{
    public class RegisterationValidator : AbstractValidator<RegisterViewModel>
    {
        public RegisterationValidator()
        {
            RuleFor(x => x.Email)
                 .NotEmpty().WithMessage("{PropertyName} is Required.")
                 .EmailAddress().WithMessage("Valid Email Address is Required.");
          

            RuleFor(x => x.Password)
                
                .NotEmpty().WithMessage("Password is Required.")
                .Length(6, 100).WithMessage("Password must be between 6 and 100 characters.")
                .Matches(@".*[A-Z]+.*").WithMessage("Your password must contain at least one uppercase letter.")
                .Matches(@".*[a-z]+.*").WithMessage("Your password must contain at least one lowercase letter.")
                .Matches(@".*[0-9]+.*").WithMessage("Your password must contain at least one number.")
                .Matches(@".*[\!\?\*\.]+.*").WithMessage("Your password must contain at least one (!? *.).")
                .Matches(@".*[a-z]+.*").WithMessage("Your password must contain at least one lowercase letter.");



            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("{PropertyName} is Required.")
                .Equal(model => model.Password).WithMessage("Confirm Password does not match password");
        }
    }
}
