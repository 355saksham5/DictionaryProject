using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace DictionaryApi.Models.MeaningApi
{
    public class Phonetic
    {

        [JsonPropertyName("text")]
        public string? PhoneticText { get; set; }

        [JsonPropertyName("audio")]
        public string? PronounceLink { get; set; }
    }
}
