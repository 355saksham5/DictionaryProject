using System.ComponentModel.DataAnnotations.Schema;

namespace DictionaryApi.Models.DTOs
{
    public class PartOfSpeech
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
	}
}
