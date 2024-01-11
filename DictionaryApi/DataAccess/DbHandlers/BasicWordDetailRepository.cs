using DictionaryApi.Data;
using DictionaryApi.DataAccess.DbHandlers.IDbHandlers;
using DictionaryApi.Helpers;
using DictionaryApi.Models.DTOs;
using DictionaryApi.Models.MeaningApi;
using Hangfire;
using System;

namespace DictionaryApi.DataAccess.DbHandlers
{
    public class BasicWordDetailRepository : IBasicWordDetailsRepository
    {
        private readonly AppDbContext context;

        public BasicWordDetailRepository(AppDbContext context)
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
            if (context.BasicWordDetails.FirstOrDefault(context => context.Word == word) != null)
            {
                context.BasicWordDetails.FirstOrDefault(context => context.Word == word).SearchedTime = DateTime.Now;
            }
            await context.SaveChangesAsync();
            return context.BasicWordDetails.FirstOrDefault(context => context.Word == word);
            
        }

        public async Task<BasicWordDetails?> GetDetailsByIdAsync(Guid wordId)
        {
            var wordDetails = await context.BasicWordDetails.FindAsync(wordId);
            return wordDetails;
        }
        public List<Guid> GetLeastRecentlyUsed()
        {
            var timeWindow = new TimeSpan(ConstantResources.timeWindow, 0, 0);
            var wordIds = context.BasicWordDetails.Where(w => DateTime.Now-w.SearchedTime>=timeWindow).Select(w => w.Id).ToList();
            return wordIds;
        }
    }
}
