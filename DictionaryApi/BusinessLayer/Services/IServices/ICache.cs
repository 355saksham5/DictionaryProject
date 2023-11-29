using DictionaryApi.Models.DTOs;

namespace DictionaryApi.BusinessLayer.Services.IServices
{
    public interface ICache 
    {
        public Task<BasicWordDetails> HandleCache(string queryWord);
    }
}
