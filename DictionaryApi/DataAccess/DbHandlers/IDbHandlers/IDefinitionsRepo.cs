using DictionaryApi.Models.DTOs;

namespace DictionaryApi.DataAccess.DbHandlers.IDbHandlers
{
    public interface IDefinitionsRepo
    {
        public Task<IEnumerable<DefinitionDto>> GetAllDefinitionsByWordIdAsync(Guid BasicWordDetailId);

        public Task<DefinitionDto> GetDefinitionByPosIdAsync(Guid PartOfSpeechId);

        public Task AddDefinitionAsync(DefinitionDto definition);

        public Task DeleteDefinitionByIdAsync(Guid id);

    }
}
