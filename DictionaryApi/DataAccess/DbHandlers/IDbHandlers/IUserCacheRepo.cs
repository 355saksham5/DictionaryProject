using DictionaryApi.Models.UserCache;
using System.Collections.Concurrent;

namespace DictionaryApi.DataAccess.DbHandlers.IDbHandlers
{
    public interface IUserCacheRepo
    {
        public Task<ConcurrentQueue<CachedWord>> GetCacheByUserId(Guid userId);
        public Task AddWordToCache(Guid userId, CachedWord word);

        public Task ClearUserCache(Guid userId);

    }
}
