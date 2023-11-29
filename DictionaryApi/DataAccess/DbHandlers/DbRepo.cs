using DictionaryApi.Data;
using DictionaryApi.DataAccess.DbHandlers.IDbHandlers;
using DictionaryApi.Models.MeaningApi;
using Microsoft.EntityFrameworkCore;

namespace DictionaryApi.DataAccess.DbHandlers
{
	public class DbRepo: IDbRepo 
	{
		private readonly AppDbContext context;

		public DbRepo(AppDbContext context)
		{
			this.context = context;
		}
		public async Task AddAsync<T>(T tuple)
		{
			await context.AddAsync(tuple);
			await context.SaveChangesAsync();
		}

		
		public Task DeleteAsync<T>(Guid wordId)
		{
			//var words = context.Set<T>().Where(tuple => tuple.Basic == wordId);
			throw new NotImplementedException();
		}

		public Task<IEnumerable<T>> GetAllAsync<T>(Guid wordId)
		{
			throw new NotImplementedException();
		}
		public Task<T> GetAsync<T>(Guid wordId)
		{
			throw new NotImplementedException();
		}

		public Task UpdateAsync<T>(T tuple)
		{
			throw new NotImplementedException();
		}
	}
}
