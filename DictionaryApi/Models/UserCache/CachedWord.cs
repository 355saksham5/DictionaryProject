using System.ComponentModel.DataAnnotations.Schema;

namespace DictionaryApi.Models.UserCache
{
	public class CachedWord
	{
		public Guid Id { get; set; }
		public Guid? WordId { get; set; }
		public string Word { get; set; }
	}
}
