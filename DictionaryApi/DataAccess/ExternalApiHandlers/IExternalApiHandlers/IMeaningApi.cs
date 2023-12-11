using DictionaryApi.Models.MeaningApi;
using Refit;
using System.Diagnostics.Metrics;

namespace DictionaryApi.ExternalApiHandlers.IExternalApiHandlers
{
    public interface IMeaningApi
    {
        [Get("/api/v2/entries/en/{queryWord}")]
        Task<List<WordDetails>> GetWordDetailsAsync(string queryWord);
    }
}
