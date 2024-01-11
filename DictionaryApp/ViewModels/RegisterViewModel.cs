using DictionaryApp.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DictionaryApp.ViewModels
{
	public class RegisterViewModel
	{

		[Required]
		[EmailAddress]
        [Display(Name = "UserName(Email)")]
        public string? Email { get; set; }


        [Required(ErrorMessage = nameof(Password)+ConstantResources.errMssgRequired)]
        [DataType(DataType.Password)]
		public string? Password { get; set; }

        [Required(ErrorMessage = nameof(ConfirmPassword) + ConstantResources.errMssgRequired)]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
		[Compare(nameof(Password),
			ErrorMessage = ConstantResources.passwordMismatchErr)]
		public string? ConfirmPassword { get; set; }

	}
}
