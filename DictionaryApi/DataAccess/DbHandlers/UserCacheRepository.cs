using DictionaryApi.Data;
using DictionaryApi.DataAccess.DbHandlers.IDbHandlers;
using DictionaryApi.Extensions;
using DictionaryApi.Helpers;
using DictionaryApi.Models.DTOs;
using DictionaryApi.Models.UserCache;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;

namespace DictionaryApi.DataAccess.DbHandlers
{
	public class UserCacheRepository : IUserCacheRepository
	{
		private readonly AppDbContext context;
		private readonly int limit = ConstantResources.limitOfCache;

		public UserCacheRepository(AppDbContext context)
		{
			this.context = context;
		}
		public async Task AddWordToCacheAsync(Guid userId, UserCache cache)
		{
			await RemoveElementOnExceedAsync(userId);
			await context.UserCache.AddAsync(cache);
			await context.SaveChangesAsync();
		}

		public async Task ClearUserCacheAsync(Guid userId)
		{
			var cache = await GetCacheByUserIdAsync(userId);
			foreach(var  cacheItem in cache)
			{
				if(cacheItem != null)
				{
                    context.UserCache.Remove(cacheItem);
                }
			}
			await context.SaveChangesAsync();
		}

		public async Task<IEnumerable<UserCache?>> GetCacheByUserIdAsync(Guid userId)
		{
			var userCache = context.UserCache.Include(ConstantResources.userCacheNavProp).Where(user => user.UserId == userId);
			return userCache;
		}

		public async Task RemoveElementOnExceedAsync(Guid userId)
		{
			var cache = await GetCacheByUserIdAsync(userId);
			while (cache.Count()>=limit)
			{
				var oldestEntry=cache.OrderBy(x => x?.SearchTime).FirstOrDefault();
				if (oldestEntry!=null)
				{
                    context.UserCache.Remove(oldestEntry);
                }
				await context.SaveChangesAsync();
			}
		}
	}
}
