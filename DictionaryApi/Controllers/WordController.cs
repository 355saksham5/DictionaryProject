using DictionaryApi.BusinessLayer.Services.IServices;
using DictionaryApi.Helpers;
using DictionaryApi.Models;
using DictionaryApi.Models.DTOs;
using DictionaryApi.Models.UserCache;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace DictionaryApi.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    [Route("api/[controller]/[action]")]
	[ApiVersion(ConstantResources.apiVersion)]
    [ResponseCache(Duration = ConstantResources.maxAgeCache)]
    [ProducesResponseType(typeof(ProblemDetails), 401)]
    [ProducesResponseType(typeof(ProblemDetails), 400)]
    [ProducesResponseType(typeof(ErrorModel), 404)]

    public class WordController : ControllerBase
    {
        private readonly IWordDetailsService wordDetailsService;
        public WordController(IWordDetailsService wordDetail)
        {
            this.wordDetailsService = wordDetail;
        }


        [HttpGet]
        [ProducesResponseType(typeof(BasicWordDetails),200)]
        public async Task<IActionResult> BasicDetails([Required] string queryWord)
        {
            var basicDetails = await wordDetailsService.GetBasicDetailsAsync(queryWord);
            return Ok(basicDetails);       
        }

        [HttpGet]
        [ProducesResponseType(typeof(BasicWordDetails), 200)]
        public async Task<IActionResult> BasicDetailsById(Guid wordId)
        {
            var basicDetails = await wordDetailsService.GetBasicDetailsByIdAsync(wordId);
			return basicDetails == null ? NotFound() : Ok(basicDetails);
		}

        [HttpGet]
        [ProducesResponseType(typeof(Antonyms), 200)]
        public async Task<IActionResult> Antonyms(Guid wordId)
        {
            var antonyms = await wordDetailsService.GetAntonymsAsync(wordId);
			return antonyms == null ? NotFound() : Ok(antonyms);
		}

        [HttpGet]
        [ProducesResponseType(typeof(Synonyms), 200)]
        public async Task<IActionResult> Synonyms(Guid wordId)
        {
            var synonyms = await wordDetailsService.GetSynonymsAsync(wordId);
			return synonyms == null ? NotFound() : Ok(synonyms);
		}

        [HttpGet]
        [ProducesResponseType(typeof(String), 200)]
        public async Task<IActionResult> Pronounciation(Guid wordId)
        {
            var pronounciation = await wordDetailsService.GetPronounciationAsync(wordId);
			return pronounciation == null ? NotFound() : Ok(pronounciation);
		}

        [HttpGet]
        [ProducesResponseType(typeof(DefinitionDto), 200)]
        public async Task<IActionResult> Definition(int index , Guid wordId)
        {
            var definition = await wordDetailsService.GetDefinitionAsync(index,wordId);
			return definition == null ? NotFound() : Ok(definition);
		}
		
	}
}
