using System.ComponentModel.DataAnnotations;

namespace DictionaryApp.ViewModels
{
	public class LogInViewModel
	{
		[Required]
		[EmailAddress]
		public string? Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string? Password { get; set; }
	}
}
