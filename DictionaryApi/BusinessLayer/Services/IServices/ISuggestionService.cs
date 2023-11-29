namespace DictionaryApi.BusinessLayer.Services.IServices
{
    public interface ISuggestionService
    {
		public Task<IEnumerable<string?>> GetSuggestions(string queryWord);
	}
}
