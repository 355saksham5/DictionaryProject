using DictionaryApi.Data;
using DictionaryApi.DataAccess.DbHandlers.IDbHandlers;
using DictionaryApi.Models.DTOs;
using DictionaryApi.Models.UserCache;
using System.Collections.Concurrent;

namespace DictionaryApi.DataAccess.DbHandlers
{
	public class UserCacheRepo : IUserCacheRepo
	{
		private readonly AppDbContext context;

		public UserCacheRepo(AppDbContext context)
		{
			this.context = context;
		}
		public async Task AddWordToCache(Guid userId, CachedWord word)
		{
			var cache = await GetCacheByUserId(userId);
			cache?.Enqueue(word);
			await context.SaveChangesAsync();
		}

		public async Task ClearUserCache(Guid userId)
		{
			var userCache = await GetCacheByUserId(userId);
			userCache?.Clear();
		}

		public async Task<ConcurrentQueue<CachedWord>>? GetCacheByUserId(Guid userId)
		{
			return context.UserCache.AsQueryable().FirstOrDefault(context => context.UserId == userId)?.Cache;
		}
	}
}
