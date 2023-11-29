using System.ComponentModel.DataAnnotations.Schema;

namespace DictionaryApi.Models.DTOs
{
    public class PhoneticDto
    {
        public Guid Id { get; set; }
        public string? PronounceLink{ get; set; }
		[ForeignKey("BasicWordDetails")]
		public Guid BasicWordDetailsId { get; set; }
	}
}
