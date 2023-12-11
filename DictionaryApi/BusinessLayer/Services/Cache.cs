using DictionaryApi.BusinessLayer.Services.IServices;
using DictionaryApi.DataAccess.DbHandlers.IDbHandlers;
using DictionaryApi.ExternalApiHandlers.IExternalApiHandlers;
using DictionaryApi.Models.DTOs;
using DictionaryApi.Models.MeaningApi;

namespace DictionaryApi.BusinessLayer.Services
{
    public class Cache : ICache
    {
        private readonly IBasicWordDetailsRepo appCache;
        private readonly IMeaningApiMapper meaningApiMapper;
		private readonly IMeaningApi meaningApi;
		public Cache(IBasicWordDetailsRepo appCache,IMeaningApiMapper meaningApiMapper, IMeaningApi meaningApi)
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

    }
}
