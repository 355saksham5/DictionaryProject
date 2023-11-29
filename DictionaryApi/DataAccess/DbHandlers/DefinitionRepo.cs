using DictionaryApi.Data;
using DictionaryApi.DataAccess.DbHandlers.IDbHandlers;
using DictionaryApi.Models.DTOs;
using DictionaryApi.Models.MeaningApi;

namespace DictionaryApi.DataAccess.DbHandlers
{
    public class DefinitionRepo : IDefinitionsRepo
    {
        private readonly AppDbContext context;

        public DefinitionRepo(AppDbContext context)
        {
            this.context = context;
        }

        public async Task AddDefinition(DefinitionDto definition)
        {
            context.Definitions.Add(definition);
            context.SaveChanges();
        }

        public async Task DeleteDefinitionById(Guid id)
        {
            var wordDefinition = context.Definitions.Where(context => context.Id == id);
            if (wordDefinition == null)
            {
                return;
            }
            context.Remove(wordDefinition);
            context.SaveChanges();
        }

        public async Task<IEnumerable<DefinitionDto>> GetAllDefinitionsByWordId(Guid basicWordDetailId)
        {
            var definitions = context.Definitions.Where(context => context.BasicWordDetailsId == basicWordDetailId);
            return definitions;
        }

        public async Task<DefinitionDto> GetDefinitionByPosId(Guid partOfSpeechId)
        {
            return context.Definitions.FirstOrDefault(context => context.Id == partOfSpeechId);
        }

        public async Task<DefinitionDto> UpdateDefinition(DefinitionDto definition)
        {
            throw new NotImplementedException();
        }
    }
}
