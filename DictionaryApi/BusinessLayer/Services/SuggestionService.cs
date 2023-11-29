using DictionaryApi.BusinessLayer.Services.IServices;
using DictionaryApi.ExternalApiHandlers.IExternalApiHandlers;

namespace DictionaryApi.BusinessLayer.Services
{
	public class SuggestionService : ISuggestionService
	{
		private readonly ISuggestionApi suggestionApi;

		private readonly int numberOfSuggestions = 5;
		public SuggestionService(ISuggestionApi suggestion)
		{
			this.suggestionApi = suggestion;
		}
		public async Task<IEnumerable<string?>> GetSuggestions(string queryWord)
		{
			var suggestions = await suggestionApi.GetSuggestions(queryWord);
			return suggestions.Select(suggestion => suggestion.Word).Take(numberOfSuggestions);


		}
	}
}
