using DictionaryApi.Models.DTOs;
using DictionaryApi.Models.MeaningApi;

namespace DictionaryApi.DataAccess.DbHandlers.IDbHandlers
{
    public interface IBasicWordDetailsRepo
    {
        public Task<BasicWordDetails> GetDetailsAsync(string word);
        public Task<BasicWordDetails> GetDetailsByIdAsync(Guid wordId);

        public Task AddDetailsAsync(BasicWordDetails wordDetails);

        public Task DeleteDetailsByIdAsync(Guid id);
        public List<Guid> GetLeastRecentlyUsed();


    }
}
