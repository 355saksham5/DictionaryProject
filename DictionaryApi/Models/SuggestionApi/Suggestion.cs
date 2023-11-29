using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace DictionaryApi.Models.SuggestionApi
{
    public class Suggestion
    {
        [JsonPropertyName("word")]
        public string? Word { get; set; }

        [JsonPropertyName("score")]
        public int Score { get; set; }
    }
}
