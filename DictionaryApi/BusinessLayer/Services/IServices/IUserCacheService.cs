using DictionaryApi.Models.UserCache;

namespace DictionaryApi.BusinessLayer.Services.IServices
{
    public interface IUserCacheService
    {
        public Task<IEnumerable<CachedWord>> GetCache();

        public Task SetCache(Guid wordId, string word);

        public Task ClearCache();
    }
}
