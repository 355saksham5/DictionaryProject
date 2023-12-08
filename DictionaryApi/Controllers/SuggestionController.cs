using DictionaryApi.BusinessLayer.Services.IServices;
using DictionaryApi.ExternalApiHandlers.IExternalApiHandlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DictionaryApi.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    public class SuggestionController : ControllerBase
    {
        private readonly ISuggestionService suggestion;
        public SuggestionController(ISuggestionService suggestion)
        {
            this.suggestion = suggestion;
        }
        [HttpGet]
        public async Task<IEnumerable<string>> Suggestions([Required]string subString)
        {
            var suggestions = await suggestion.GetSuggestions(subString);
            return suggestions;
        }
    }
}
