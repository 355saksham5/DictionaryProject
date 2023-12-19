namespace DictionaryApp.Models
{
	public class BasicWordDetails 
	{
		public Guid Id { get; set; }
		public string? Word { get; set; } = null;
		public string? DefaultDefinition { get; set; } = null;
		public string? DefaultPhoneticsText { get; set; } = null;
		public string? Origin { get; set; } = null;
		public bool IsPronounceLnkPresent { get; set; } = false;
		public int NumberOfDefinitions { get; set; }
	}
}

