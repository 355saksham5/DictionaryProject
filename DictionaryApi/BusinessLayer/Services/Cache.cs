using DictionaryApi.BusinessLayer.Services.IServices;
using DictionaryApi.DataAccess.DbHandlers.IDbHandlers;
using DictionaryApi.ExternalApiHandlers.IExternalApiHandlers;
using DictionaryApi.Models.DTOs;
using DictionaryApi.Models.MeaningApi;
using Hangfire;

namespace DictionaryApi.BusinessLayer.Services
{
    public class Cache : ICache
    {
        private readonly IBasicWordDetailsRepository appCache;
        private readonly IMeaningApiMapper meaningApiMapper;
		private readonly IMeaningApi meaningApi;
        private readonly IDefinitionsRepository definitionsRepo;
        private readonly IPhoneticAudioRepository phoneticAudiosRepo;
        
		public  Cache(IBasicWordDetailsRepository appCache,IMeaningApiMapper meaningApiMapper, IMeaningApi meaningApi)
        {
            this.appCache = appCache;
            this.meaningApiMapper = meaningApiMapper;
			this.meaningApi = meaningApi;
		}
        public async Task<BasicWordDetails> HandleCacheAsync(string queryWord)
        {
            var wordCached = await appCache.GetDetailsAsync(queryWord);
            if (wordCached == null)
            {
                var wordDetails = await meaningApi.GetWordDetailsAsync(queryWord);
                wordCached=await meaningApiMapper.MapBasicWordDetailsAsync(wordDetails); 
            }
            return wordCached;
        }
        public void DeleteFromCache()
        {
            var wordIdsToBeCleared = appCache.GetLeastRecentlyUsed();
            foreach(var wordId in wordIdsToBeCleared)
            {
                appCache.DeleteDetailsByIdAsync(wordId);
            }
                
        }
    }
}
