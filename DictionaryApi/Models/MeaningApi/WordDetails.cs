using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace DictionaryApi.Models.MeaningApi
{
    public class WordDetails
    {
        public Guid Id { get; set; }

        [JsonPropertyName("word")]
        public string? Word { get; set; }
        [JsonPropertyName("phonetics")]
        public IEnumerable<Phonetic>? Phonetics { get; set; }
        [JsonPropertyName("origin")]
        public string? Origin { get; set; }
        [JsonPropertyName("meanings")]
        public List<Meaning>? Meanings { get; set; }
    }
}
