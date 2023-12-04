using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations.Schema;

namespace DictionaryApi.Models.UserCache
{
	public class UserCache
	{
		public Guid Id { get; set; }

		[ForeignKey("UserId")]
		public Guid? UserId { get; set; }

		public ConcurrentQueue<CachedWord>? Cache {  get; set; }
	}
}
