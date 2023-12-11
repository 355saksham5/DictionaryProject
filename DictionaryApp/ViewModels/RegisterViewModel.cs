using DictionaryApp.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DictionaryApp.ViewModels
{
	public class RegisterViewModel
	{

		[Required]
		[EmailAddress]
		public string? Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string? Password { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Confirm password")]
		[Compare(nameof(Password),
			ErrorMessage = ConstantResources.passwordMismatchErr)]
		public string? ConfirmPassword { get; set; }

	}
}
