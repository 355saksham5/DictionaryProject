using DictionaryApi.Models.UserCache;

namespace DictionaryApi.BusinessLayer.Services.IServices
{
    public interface IUserCacheService
    {
        public Task<IEnumerable<CachedWord>> GetCacheAsync();

        public Task SetCacheAsync(Guid wordId, string word);

        public Task ClearCacheAsync();
    }
}
