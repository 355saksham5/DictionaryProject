using DictionaryApi.Models.SuggestionApi;
using Refit;

namespace DictionaryApi.ExternalApiHandlers.IExternalApiHandlers
{
    public interface ISuggestionApi
    {
        [Get("/sug?s={querySubWord}")]
        Task<List<Suggestion>> GetSuggestions(string querySubWord);
    }
}
