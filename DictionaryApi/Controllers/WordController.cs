using DictionaryApi.BusinessLayer.Services.IServices;
using DictionaryApi.Models.DTOs;
using DictionaryApi.Models.UserCache;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DictionaryApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
	[ApiVersion("1.0")]
	public class WordController : ControllerBase
    {
        private readonly IWordDetailsService wordDetails;
        public WordController(IWordDetailsService wordDetail)
        {
            this.wordDetails = wordDetail;
        }


        [HttpGet]
        public async Task<BasicWordDetails> BasicDetails([Required] string queryWord)
        {
            var basicDetails = await wordDetails.GetBasicDetails(queryWord);
            return basicDetails;           
        }

        [HttpGet]
        public async Task<BasicWordDetails> BasicDetailsById([Required] Guid wordId)
        {
            var basicDetails = await wordDetails.GetBasicDetailsById(wordId);
            return basicDetails;
        }

        [HttpGet]

        public async Task<IEnumerable<String>> Antonyms([Required] Guid wordId)
        {
            var antonyms = await wordDetails.GetAntonyms(wordId);
            return antonyms;
        }

        [HttpGet]

        public async Task<IEnumerable<String>> Synonyms([Required] Guid wordId)
        {
            var synonyms = await wordDetails.GetSynonyms(wordId);
            return synonyms;
        }

        [HttpGet]

        public async Task<String> Pronounciation([Required] Guid wordId)
        {
            var pronounciation = await wordDetails.GetPronounciation(wordId);
            return pronounciation;
        }

        [HttpGet]
        public async Task<DefinitionDto> Definition([Required] int index , [Required] Guid wordId)
        {
            var definition = await wordDetails.GetDefinition(index,wordId);
            return definition;
        }
		
	}
}
