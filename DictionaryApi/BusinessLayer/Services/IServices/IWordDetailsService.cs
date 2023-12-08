using DictionaryApi.Models.DTOs;

namespace DictionaryApi.BusinessLayer.Services.IServices
{
    public interface IWordDetailsService
    {
        public Task<BasicWordDetails> GetBasicDetails(string queryWord);
        public Task<BasicWordDetails> GetBasicDetailsById(Guid wordId);
        public Task<IEnumerable<String>> GetAntonyms(Guid wordId);
        public Task<IEnumerable<String>> GetSynonyms(Guid wordId);
        public Task<String> GetPronounciation(Guid wordId);
        public Task<DefinitionDto> GetDefinition(int index, Guid wordId);
    }
}
