using DictionaryApi.BusinessLayer.Services.IServices;
using DictionaryApi.DataAccess.DbHandlers.IDbHandlers;
using DictionaryApi.Models.DTOs;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using DictionaryApi.Helpers;
using Refit;
using System.Net;
using System.Diagnostics.CodeAnalysis;

namespace DictionaryApi.BusinessLayer.Services
{
    public class WordDetailsService : IWordDetailsService
    {
        private readonly IBasicWordDetailsRepository wordDetails;
        private readonly IDefinitionsRepository definitions;
        private readonly IPhoneticAudioRepository phoneticAudio;
        private readonly IAntonymsRepository antonymsRepo;
		private readonly ISynonymsRepository synonymsRepo;
		private readonly ICache appCache;
		private readonly IUserCacheService userCache;
		public WordDetailsService(IBasicWordDetailsRepository wordDetails,IDefinitionsRepository definition, IPhoneticAudioRepository phoneticAudio, ICache appCache,
            IAntonymsRepository antonyms, ISynonymsRepository synonyms, IUserCacheService userCache)
        {
            this.wordDetails = wordDetails;
            this.definitions = definition;
            this.phoneticAudio = phoneticAudio;
            this.appCache = appCache;
            this.antonymsRepo = antonyms;
            this.synonymsRepo = synonyms;
            this.userCache = userCache;
        }

        public async Task<IEnumerable<string>> GetAntonymsAsync(Guid wordId)
        { 
            if(!await ValidateWordIdAsync(wordId))
            {
                return null;
            }
            var antonyms = await antonymsRepo.GetAntonymsAsync(wordId);
            return antonyms;
        }

        public async Task<BasicWordDetails?> GetBasicDetailsAsync(string queryWord)
        {
            var basicDetails = await appCache.HandleCacheAsync(queryWord);
            if(basicDetails != null)
            {
                await userCache.SetCacheAsync(basicDetails.Id, basicDetails.Word); 
            }
			return basicDetails;
        }

        public async Task<BasicWordDetails?> GetBasicDetailsByIdAsync(Guid wordId)
        {
			var details = await wordDetails.GetDetailsByIdAsync(wordId);
            return details;
        }

        public async Task<DefinitionDto> GetDefinitionAsync(int? index, Guid wordId)
        { 
            if(index == null)
            {
                throw new BadHttpRequestException(ConstantResources.errOnIndexNull);
            }
			if (!await ValidateWordIdAsync(wordId))
			{
				return null;
			}
			var allDefinitions = await definitions.GetAllDefinitionsByWordIdAsync(wordId);
            if(index<allDefinitions.Count())
            {
				var definition = allDefinitions.ToList()[index.Value];
				return definition;
			}
            else
            {
                throw new AnyHttpException(HttpStatusCode.BadRequest,ConstantResources.errorOnInvalidIndex);
            }
        }

        public async Task<string> GetPronounciationAsync(Guid wordId)
        {
			if (!await ValidateWordIdAsync(wordId))
			{
				return null;
			}
			var pronounciation = await phoneticAudio.GetPronounciationByWordIdAsync(wordId);
            return pronounciation.PronounceLink;
        }

        public async Task<IEnumerable<string>> GetSynonymsAsync(Guid wordId)
        {
			if (!await ValidateWordIdAsync(wordId))
			{
				return null;
			}
			var synonyms = await synonymsRepo.GetSynonymsAsync(wordId);
			return synonyms;
		}

        
        private async Task<bool> ValidateWordIdAsync(Guid wordId)
        {
            if(wordId==null)
            {
                throw new AnyHttpException(HttpStatusCode.BadRequest, ConstantResources.errorOnInvalidWordId);
            }
            var details = await wordDetails.GetDetailsByIdAsync(wordId);
            if (details != null)
            {
                return true;
            }
            else
            {
                throw new AnyHttpException(HttpStatusCode.BadRequest,ConstantResources.errorOnInvalidWordId);
            }
		}
        
    }
}
