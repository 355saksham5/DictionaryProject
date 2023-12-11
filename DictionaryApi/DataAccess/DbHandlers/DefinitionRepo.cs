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

        public async Task AddDefinitionAsync(DefinitionDto definition)
        {
            await context.Definitions.AddAsync(definition);
            await context.SaveChangesAsync();
        }

        public async Task DeleteDefinitionByIdAsync(Guid id)
        {
            var wordDefinition = context.Definitions.Where(context => context.Id == id);
            if (wordDefinition == null)
            {
                return;
            }
            context.Remove(wordDefinition);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<DefinitionDto>> GetAllDefinitionsByWordIdAsync(Guid basicWordDetailId)
        {
            var definitions = context.Definitions.Where(context => context.BasicWordDetailsId == basicWordDetailId);
            return definitions;
        }

        public async Task<DefinitionDto?> GetDefinitionByPosIdAsync(Guid partOfSpeechId)
        {
            return context.Definitions.FirstOrDefault(context => context.Id == partOfSpeechId);
        }

    }
}
