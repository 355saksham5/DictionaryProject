using DictionaryApi.Models.DTOs;

namespace DictionaryApi.DataAccess.DbHandlers.IDbHandlers
{
    public interface IDefinitionsRepo
    {
        public Task<IEnumerable<DefinitionDto>> GetAllDefinitionsByWordId(Guid BasicWordDetailId);

        public Task<DefinitionDto> GetDefinitionByPosId(Guid PartOfSpeechId);

        public Task AddDefinition(DefinitionDto definition);

        public Task DeleteDefinitionById(Guid id);

        public Task<DefinitionDto> UpdateDefinition(DefinitionDto definition);
    }
}
