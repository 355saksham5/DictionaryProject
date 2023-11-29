using DictionaryApi.BusinessLayer.Services.IServices;
using DictionaryApi.DataAccess.DbHandlers.IDbHandlers;
using DictionaryApi.Models.DTOs;

namespace DictionaryApi.BusinessLayer.Services
{
    public class WordDetailsService : IWordDetailsService
    {
        private readonly IBasicWordDetailsRepo wordDetails;
        private readonly IDefinitionsRepo definitions;
        private readonly IPhoneticAudiosRepo phoneticAudio;
        private readonly IAntonymsRepo antonymsRepo;
		private readonly ISynonymsRepo synonymsRepo;
		private readonly ICache appCache;
        public WordDetailsService(IBasicWordDetailsRepo wordDetails,IDefinitionsRepo definition, IPhoneticAudiosRepo phoneticAudio, ICache appCache,
            IAntonymsRepo antonyms, ISynonymsRepo synonyms)
        {
            this.wordDetails = wordDetails;
            this.definitions = definition;
            this.phoneticAudio = phoneticAudio;
            this.appCache = appCache;
            this.antonymsRepo = antonyms;
            this.synonymsRepo = synonyms;
        }

        public async Task<IEnumerable<string>> GetAntonyms(Guid wordId)
        {
            var antonyms = await antonymsRepo.GetAntonyms(wordId);
            return antonyms;
        }

        public async Task<BasicWordDetails> GetBasicDetails(string queryWord)
        {
            var basicDetails =await appCache.HandleCache(queryWord);
			return basicDetails;
        }

        public async Task<DefinitionDto> GetDefinition(int index, Guid wordId)
        {
            var allDefinitions = await definitions.GetAllDefinitionsByWordId(wordId);
            if(index<allDefinitions.Count())
            {
				var definition = allDefinitions.ToList()[index];
				return definition;
			}
            return null;
        }

        public async Task<string> GetPronounciation(Guid wordId)
        {
            var pronounciation = await phoneticAudio.GetPronounciationByWordId(wordId);
            return pronounciation?.PronounceLink;
        }

        public async Task<IEnumerable<string>> GetSynonyms(Guid wordId)
        {
			var synonyms = await synonymsRepo.GetSynonyms(wordId);
			return synonyms;
		}
    }
}
