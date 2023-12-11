using DictionaryApi.Models.DTOs;

namespace DictionaryApi.DataAccess.DbHandlers.IDbHandlers
{
	public interface ISynonymsRepo
	{
		public Task AddSynonymsAsync(Synonyms synonyms);
		public Task<IEnumerable<string>> GetSynonymsAsync(Guid BasicWordDetailsId);
	}
}
