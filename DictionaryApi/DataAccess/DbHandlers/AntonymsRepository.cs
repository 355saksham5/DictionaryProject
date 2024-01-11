using DictionaryApi.Data;
using DictionaryApi.DataAccess.DbHandlers.IDbHandlers;
using DictionaryApi.Models.DTOs;

namespace DictionaryApi.DataAccess.DbHandlers
{
	public class AntonymsRepository : IAntonymsRepository
	{
		private readonly AppDbContext context;

		public AntonymsRepository(AppDbContext context)
		{
			this.context = context;
		}
		public async Task AddAntonymsAsync(Antonyms antonym)
		{
			await context.Antonyms.AddAsync(antonym);
		}

		public async Task<IEnumerable<string?>?> GetAntonymsAsync(Guid BasicWordDetailsId)
		{
			var words = context.Antonyms.Where(antonym=>antonym.BasicWordDetailsId==BasicWordDetailsId)
				.Select(antonym=>antonym.Antonym).SelectMany(word=>word);
			return words?.Select(word=>word.Word).ToList();

		}
	}
}
