using DictionaryApi.Data;
using DictionaryApi.DataAccess.DbHandlers.IDbHandlers;
using DictionaryApi.Models.DTOs;

namespace DictionaryApi.DataAccess.DbHandlers
{
	public class SynonymsRepository : ISynonymsRepository
	{
		private readonly AppDbContext context;

		public SynonymsRepository(AppDbContext context)
		{
			this.context = context;
		}
		public async Task AddSynonymsAsync(Synonyms synonym)
		{
			await context.Synonyms.AddAsync(synonym);
			await context.SaveChangesAsync();
		}

		public async Task<IEnumerable<string?>> GetSynonymsAsync(Guid BasicWordDetailsId)
		{
			var words = context.Synonyms.Where(synonym => synonym.BasicWordDetailsId == BasicWordDetailsId).Select(synonym => synonym.Synonym).SelectMany(word => word);
			return words.Select(word => word.Word).ToList();
		}
	}
}
