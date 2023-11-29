using System.ComponentModel.DataAnnotations.Schema;

namespace DictionaryApi.Models.DTOs
{
	public class Synonyms
	{
		public Guid Id { get; set; }
		public List<Words>? Synonym { get; set; }
		[ForeignKey("BasicWordDetails")]
		public Guid BasicWordDetailsId { get; set; }
	}
}
