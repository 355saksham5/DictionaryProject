using DictionaryApi.BusinessLayer.Services.IServices;
using DictionaryApi.ExternalApiHandlers.IExternalApiHandlers;
using Microsoft.AspNetCore.Mvc;

namespace DictionaryApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SuggestionController : ControllerBase
    {
        private readonly ISuggestionService suggestion;
        public SuggestionController(ISuggestionService suggestion)
        {
            this.suggestion = suggestion;
        }
        [HttpGet("{subString}")]
        public async Task<IEnumerable<string>> Suggestions(string subString)
        {
            var suggestions = await suggestion.GetSuggestions(subString);
            return suggestions;
        }
    }
}
