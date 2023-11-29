using DictionaryApi.Models.DTOs;
using DictionaryApi.Models.MeaningApi;

namespace DictionaryApi.DataAccess.DbHandlers.IDbHandlers
{
    public interface IBasicWordDetailsRepo
    {
        public Task<BasicWordDetails> GetDetails(string word);

        public Task AddDetails(BasicWordDetails wordDetails);

        public Task DeleteDetailsById(Guid id);

        public Task<BasicWordDetails> UpdateDetails(BasicWordDetails wordDetails);


	}
}
