using System.ComponentModel.DataAnnotations.Schema;

namespace DictionaryApp.Models
{
	public class Definition
	{
		public string? DefinitionText { get; set; }
		public string? Example { get; set; }
		public PartOfSpeech? PartOfSpeech { get; set; }

	}
}
