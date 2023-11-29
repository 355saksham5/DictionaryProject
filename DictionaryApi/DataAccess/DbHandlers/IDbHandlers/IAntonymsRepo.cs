using DictionaryApi.Models.DTOs;

namespace DictionaryApi.DataAccess.DbHandlers.IDbHandlers
{
	public interface IAntonymsRepo
	{
		public Task AddAntonyms(Antonyms antonyms);

		public Task<IEnumerable<string>> GetAntonyms(Guid BasicWordDetailsId);
	}
}
