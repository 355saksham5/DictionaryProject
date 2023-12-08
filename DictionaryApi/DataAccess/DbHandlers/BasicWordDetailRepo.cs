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

        public async Task AddDetails(BasicWordDetails wordDetail)
		{
			await context.AddAsync(wordDetail);
            await context.SaveChangesAsync();
        }

        public async Task DeleteDetailsById(Guid id)
        {
           context.Remove(context.BasicWordDetails.Where(word=>word.Id==id));
			await context.SaveChangesAsync();
		}

        public async Task<BasicWordDetails?> GetDetails(string word)
        {
            return context.BasicWordDetails.FirstOrDefault(context => context.Word == word);
        }

        public async Task<BasicWordDetails?> GetDetailsById(Guid wordId)
        {
            var wordDetails = await context.BasicWordDetails.FindAsync(wordId);
            return wordDetails;
        }

        public async Task<BasicWordDetails> UpdateDetails(BasicWordDetails wordDetails)
        {
            throw new NotImplementedException();
        }
    }
}
