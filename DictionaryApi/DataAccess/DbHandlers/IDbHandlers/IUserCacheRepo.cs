using DictionaryApi.Models.UserCache;
using System.Collections.Concurrent;

namespace DictionaryApi.DataAccess.DbHandlers.IDbHandlers
{
    public interface IUserCacheRepo
    {
        public Task<IEnumerable<UserCache>> GetCacheByUserIdAsync(Guid userId);
        public Task AddWordToCacheAsync(Guid userId, UserCache cache);

        public Task ClearUserCacheAsync(Guid userId);

    }
}
