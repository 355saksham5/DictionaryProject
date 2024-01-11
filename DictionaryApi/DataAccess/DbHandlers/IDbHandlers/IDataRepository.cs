namespace DictionaryApi.DataAccess.DbHandlers.IDbHandlers
{
    public interface IDataRepository
    {
        public Task AddAsync<T>(T obj);

        public Task DeleteAsync<T>(Guid id);
        public Task<int> CountAsync();
        public Task<int> GetCountAsync();
            
    }
}
