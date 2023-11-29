using System.ComponentModel.DataAnnotations.Schema;

namespace DictionaryApi.Models.DTOs
{
	public class Antonyms
	{
		public Guid Id { get; set; }
		public List<Words>? Antonym { get; set; }
		[ForeignKey("BasicWordDetails")]
		public Guid BasicWordDetailsId { get; set; }
	}
}
