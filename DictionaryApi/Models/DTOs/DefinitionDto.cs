using System.ComponentModel.DataAnnotations.Schema;

namespace DictionaryApi.Models.DTOs
{
    public class DefinitionDto
    {
        public Guid Id { get; set; }
        public string? DefinitionText { get; set; }
        public string? Example { get; set;}
        public PartOfSpeech? PartOfSpeech { get; set; }

        [ForeignKey("BasicWordDetails")]
        public Guid BasicWordDetailsId { get; set; }
    }
}
