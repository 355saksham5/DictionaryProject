using DictionaryApi.BusinessLayer.Services.IServices;
using DictionaryApi.DataAccess.DbHandlers.IDbHandlers;
using DictionaryApi.ExternalApiHandlers.IExternalApiHandlers;
using DictionaryApi.Models.UserCache;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Security.Claims;

namespace DictionaryApi.BusinessLayer.Services
{
	public class UserCacheService : IUserCacheService
	{
		private readonly IUserCacheRepo userCacheRepo;
		private CachedWord cachedWord;
		private Guid userId { get; set; }
		public UserCacheService(IUserCacheRepo userCacheRepo, IHttpContextAccessor contextAccessor,
			CachedWord cachedWord)
		{
			this.userCacheRepo = userCacheRepo;
			userId = new Guid(contextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
			this.cachedWord = cachedWord;
		}

		public async Task ClearCache()
		{
			await userCacheRepo.ClearUserCache(userId);
		}

		public async Task<IEnumerable<CachedWord>> GetCache()
		{
			var userCache = await userCacheRepo.GetCacheByUserId(userId);
			var cachedWords = userCache.Select(userCache => userCache.Cache);
			return cachedWords;
		}

		public async Task SetCache(Guid wordId, string word)
		{
			cachedWord.WordId = wordId;
			cachedWord.Word = word;
			cachedWord.Id=Guid.NewGuid();
			var userCache = new UserCache { Id = Guid.NewGuid(), UserId = userId, SearchTime = DateTime.Now, Cache = cachedWord };
			var isWordInHistory = await IsAlreadyCached(word);
			if(isWordInHistory)
			{
				return;
			}
			else
			{
                await userCacheRepo.AddWordToCache(userId, userCache);
            }
		}
        public async Task<bool> IsAlreadyCached(String word)
        {
			var userCache = await userCacheRepo.GetCacheByUserId(userId);
			var cachedWords = userCache.Where(c => c.Cache.Word == word);
            if (cachedWords!=null && cachedWords.Count()!=0)
			{
				return true;
			}
			else
			{
				return false;
			}
        }
    }
}
