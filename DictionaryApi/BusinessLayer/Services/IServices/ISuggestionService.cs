namespace DictionaryApi.BusinessLayer.Services.IServices
{
    public interface ISuggestionService
    {
		public Task<IEnumerable<string?>> GetSuggestionsAsync(string queryWord);
	}
}
