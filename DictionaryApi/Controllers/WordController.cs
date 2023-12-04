using DictionaryApi.BusinessLayer.Services.IServices;
using DictionaryApi.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DictionaryApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[action]")]
    public class WordController : ControllerBase
    {
        private readonly IWordDetailsService wordDetails;
        public WordController(IWordDetailsService wordDetail)
        {
            this.wordDetails = wordDetail;
        }


        [HttpGet("{queryWord}")]

        public async Task<BasicWordDetails> BasicDetails(string queryWord)
        {
            var basicDetails = await wordDetails.GetBasicDetails(queryWord);
            return basicDetails;           
        }

        [HttpGet("{wordId}")]

        public async Task<IEnumerable<String>> Antonyms(Guid wordId)
        {
            var antonyms = await wordDetails.GetAntonyms(wordId);
            return antonyms;
        }

        [HttpGet("{wordId}")]

        public async Task<IEnumerable<String>> Synonyms(Guid wordId)
        {
            var synonyms = await wordDetails.GetSynonyms(wordId);
            return synonyms;
        }

        [HttpGet("{wordId}")]

        public async Task<String> Pronounciation(Guid wordId)
        {
            var pronounciation = await wordDetails.GetPronounciation(wordId);
            return pronounciation;
        }

        [HttpGet("{index}&{wordId}")]
        public async Task<DefinitionDto> Definition(int index , Guid wordId)
        {
            var definition = await wordDetails.GetDefinition(index,wordId);
            return definition;
        }

        
    }
}
