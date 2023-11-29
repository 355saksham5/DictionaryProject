namespace DictionaryApplication.Models
{
	public class BasicWordDetails
	{
		    public Guid Id { get; set; }
		    public string? Word { get; set; }
			public string? DefaultDefinition { get; set; }
			public string? DefaultPhoneticsText { get; set; }
			public string? Origin { get; set; }
			public bool IsPronounceLnkPresent { get; set; }
			public List<PartOfSpeech>? PartOfSpeeches { get; set; }
		}
}

