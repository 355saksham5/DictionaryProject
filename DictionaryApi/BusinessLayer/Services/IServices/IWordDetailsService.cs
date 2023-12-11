using DictionaryApi.Models.DTOs;

namespace DictionaryApi.BusinessLayer.Services.IServices
{
    public interface IWordDetailsService
    {
        public Task<BasicWordDetails> GetBasicDetailsAsync(string queryWord);
        public Task<BasicWordDetails> GetBasicDetailsByIdAsync(Guid wordId);
        public Task<IEnumerable<String>> GetAntonymsAsync(Guid wordId);
        public Task<IEnumerable<String>> GetSynonymsAsync(Guid wordId);
        public Task<String> GetPronounciationAsync(Guid wordId);
        public Task<DefinitionDto> GetDefinitionAsync(int index, Guid wordId);
    }
}
