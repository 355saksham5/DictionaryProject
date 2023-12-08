using DictionaryApi.Models.UserCache;
using System.Collections.Concurrent;

namespace DictionaryApi.DataAccess.DbHandlers.IDbHandlers
{
    public interface IUserCacheRepo
    {
        public Task<IEnumerable<UserCache>> GetCacheByUserId(Guid userId);
        public Task AddWordToCache(Guid userId, UserCache cache);

        public Task ClearUserCache(Guid userId);

    }
}
