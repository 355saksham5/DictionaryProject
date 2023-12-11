using DictionaryApi.BusinessLayer.Services.IServices;
using DictionaryApi.ExternalApiHandlers.IExternalApiHandlers;
using DictionaryApi.Helpers;
using DictionaryApi.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DictionaryApi.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion(ConstantResources.apiVersion)]
    public class SuggestionController : ControllerBase
    {
        private readonly ISuggestionService suggestion;
        public SuggestionController(ISuggestionService suggestion)
        {
            this.suggestion = suggestion;
        }
        [HttpGet]
        public async Task<IActionResult> Suggestions([Required]string subString)
        {
            var suggestions = await suggestion.GetSuggestionsAsync(subString);
			return suggestions == null ? NotFound() : Ok(suggestions);
		}
    }
}
