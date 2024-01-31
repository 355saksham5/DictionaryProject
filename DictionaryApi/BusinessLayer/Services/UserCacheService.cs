using DictionaryApi.BusinessLayer.Services.IServices;
using DictionaryApi.DataAccess.DbHandlers.IDbHandlers;
using DictionaryApi.ExternalApiHandlers.IExternalApiHandlers;
using DictionaryApi.Helpers;
using DictionaryApi.Models.UserCache;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Security.Claims;

namespace DictionaryApi.BusinessLayer.Services
{
	public class UserCacheService : IUserCacheService
	{
		private readonly IUserCacheRepository userCacheRepo;
		private CachedWord cachedWord;
		private Guid userId { get; set; }
		public UserCacheService(IUserCacheRepository userCacheRepo, IHttpContextAccessor contextAccessor)
		{
			this.userCacheRepo = userCacheRepo;
			userId = new Guid(contextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type.Equals(ConstantResources.claimInJwt)).Value);
			this.cachedWord = new CachedWord();
		}

		public async Task ClearCacheAsync()
		{
			await userCacheRepo.ClearUserCacheAsync(userId);
		}

		public async Task<IEnumerable<CachedWord>> GetCacheAsync()
		{
			var userCache = await userCacheRepo.GetCacheByUserIdAsync(userId);
			var cachedWords = userCache.Select(userCache => userCache.Cache);
			return cachedWords;
		}

		public async Task SetCacheAsync(Guid wordId, string word)
		{
			cachedWord.WordId = wordId;
			cachedWord.Word = word;
			cachedWord.Id=Guid.NewGuid();
			var userCache = new UserCache { Id = Guid.NewGuid(), UserId = userId, SearchTime = DateTime.Now, Cache = cachedWord };
			var isWordInHistory = await IsAlreadyCachedAsync(word);
			if(isWordInHistory)
			{
				return;
			}
			else
			{
                await userCacheRepo.AddWordToCacheAsync(userId, userCache);
            }
		}
        private async Task<bool> IsAlreadyCachedAsync(String word)
        {
			var userCache = await userCacheRepo.GetCacheByUserIdAsync(userId);
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
