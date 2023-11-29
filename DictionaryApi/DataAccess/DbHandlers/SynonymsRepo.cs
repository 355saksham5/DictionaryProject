using DictionaryApi.Data;
using DictionaryApi.DataAccess.DbHandlers.IDbHandlers;
using DictionaryApi.Models.DTOs;

namespace DictionaryApi.DataAccess.DbHandlers
{
	public class SynonymsRepo : ISynonymsRepo
	{
		private readonly AppDbContext context;

		public SynonymsRepo(AppDbContext context)
		{
			this.context = context;
		}
		public async Task AddSynonyms(Synonyms synonym)
		{
			await context.Synonyms.AddAsync(synonym);
		}

		public async Task<IEnumerable<string>> GetSynonyms(Guid BasicWordDetailsId)
		{
			var words = context.Synonyms.Where(synonym => synonym.BasicWordDetailsId == BasicWordDetailsId).Select(synonym => synonym.Synonym).SelectMany(word => word);
			return words.Select(word => word.Word).ToList();
		}
	}
}
