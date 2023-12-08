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
	public class UserCacheRepo : IUserCacheRepo
	{
		private readonly AppDbContext context;
		private readonly int limit = ConstantResources.limitOfCache;

		public UserCacheRepo(AppDbContext context)
		{
			this.context = context;
		}
		public async Task AddWordToCache(Guid userId, UserCache cache)
		{
			await RemoveElementOnExceed(userId);
			await context.UserCache.AddAsync(cache);
			await context.SaveChangesAsync();
		}

		public async Task ClearUserCache(Guid userId)
		{
			var cache = await GetCacheByUserId(userId);
			foreach(var  cacheItem in cache)
			{
				if(cacheItem != null)
				{
                    context.UserCache.Remove(cacheItem);
                }
			}
			await context.SaveChangesAsync();
		}

		public async Task<IEnumerable<UserCache?>> GetCacheByUserId(Guid userId)
		{
			var userCache = context.UserCache.Include("Cache").Where(user => user.UserId == userId);
			return userCache;
		}

		public async Task RemoveElementOnExceed(Guid userId)
		{
			var cache = await GetCacheByUserId(userId);
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
