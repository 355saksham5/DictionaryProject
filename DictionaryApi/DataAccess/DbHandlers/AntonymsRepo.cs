using DictionaryApi.Data;
using DictionaryApi.DataAccess.DbHandlers.IDbHandlers;
using DictionaryApi.Models.DTOs;

namespace DictionaryApi.DataAccess.DbHandlers
{
	public class AntonymsRepo : IAntonymsRepo
	{
		private readonly AppDbContext context;

		public AntonymsRepo(AppDbContext context)
		{
			this.context = context;
		}
		public async Task AddAntonyms(Antonyms antonym)
		{
			await context.Antonyms.AddAsync(antonym);
		}

		public async Task<IEnumerable<string>> GetAntonyms(Guid BasicWordDetailsId)
		{
			var words = context.Antonyms.Where(antonym=>antonym.BasicWordDetailsId==BasicWordDetailsId)
				.Select(antonym=>antonym.Antonym).SelectMany(word=>word);
			return words.Select(word=>word.Word).ToList();

		}
	}
}
