namespace DictionaryApi.DataAccess.DbHandlers.IDbHandlers
{
	public interface IDbRepo
	{
		public Task AddAsync<T>(T tuple);
		public Task<T> GetAsync<T>(Guid wordId);
		public Task<IEnumerable<T>> GetAllAsync<T>(Guid wordId);
		public Task DeleteAsync<T>(Guid wordId);
		public Task UpdateAsync<T>(T tuple);

	}
}
