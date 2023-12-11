using DictionaryApi.Data;
using DictionaryApi.DataAccess.DbHandlers.IDbHandlers;
using DictionaryApi.Models.DTOs;
using DictionaryApi.Models.MeaningApi;
using System;

namespace DictionaryApi.DataAccess.DbHandlers
{
    public class BasicWordDetailRepo : IBasicWordDetailsRepo
    {
        private readonly AppDbContext context;

        public BasicWordDetailRepo(AppDbContext context)
        {
            this.context = context;
        }

        public async Task AddDetailsAsync(BasicWordDetails wordDetail)
		{
			await context.AddAsync(wordDetail);
            await context.SaveChangesAsync();
        }

        public async Task DeleteDetailsByIdAsync(Guid id)
        {
           context.Remove(context.BasicWordDetails.Where(word=>word.Id==id));
			await context.SaveChangesAsync();
		}

        public async Task<BasicWordDetails?> GetDetailsAsync(string word)
        {
            return context.BasicWordDetails.FirstOrDefault(context => context.Word == word);
        }

        public async Task<BasicWordDetails?> GetDetailsByIdAsync(Guid wordId)
        {
            var wordDetails = await context.BasicWordDetails.FindAsync(wordId);
            return wordDetails;
        }
    }
}
