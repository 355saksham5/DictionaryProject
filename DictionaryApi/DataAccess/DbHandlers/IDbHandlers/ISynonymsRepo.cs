using DictionaryApi.Models.DTOs;

namespace DictionaryApi.DataAccess.DbHandlers.IDbHandlers
{
	public interface ISynonymsRepo
	{
		public Task AddSynonyms(Synonyms synonyms);
		public Task<IEnumerable<string>> GetSynonyms(Guid BasicWordDetailsId);
	}
}
