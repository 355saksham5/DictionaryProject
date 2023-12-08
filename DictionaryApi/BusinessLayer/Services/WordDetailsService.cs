using DictionaryApi.BusinessLayer.Services.IServices;
using DictionaryApi.DataAccess.DbHandlers.IDbHandlers;
using DictionaryApi.Models.DTOs;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

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
		private readonly IUserCacheService userCache;
		public WordDetailsService(IBasicWordDetailsRepo wordDetails,IDefinitionsRepo definition, IPhoneticAudiosRepo phoneticAudio, ICache appCache,
            IAntonymsRepo antonyms, ISynonymsRepo synonyms, IUserCacheService userCache)
        {
            this.wordDetails = wordDetails;
            this.definitions = definition;
            this.phoneticAudio = phoneticAudio;
            this.appCache = appCache;
            this.antonymsRepo = antonyms;
            this.synonymsRepo = synonyms;
            this.userCache = userCache;
        }

        public async Task<IEnumerable<string>> GetAntonyms(Guid wordId)
        {
            var antonyms = await antonymsRepo.GetAntonyms(wordId);
            return antonyms;
        }

        public async Task<BasicWordDetails> GetBasicDetails(string queryWord)
        {
            var basicDetails = await appCache.HandleCache(queryWord);
            if(basicDetails != null)
            {
                await userCache.SetCache(basicDetails.Id, basicDetails.Word); 
            }
			return basicDetails;
        }

        public async Task<BasicWordDetails> GetBasicDetailsById(Guid wordId)
        {
            var details = await wordDetails.GetDetailsById(wordId);
            return details;
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
