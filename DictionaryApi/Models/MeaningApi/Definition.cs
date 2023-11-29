using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DictionaryApi.Models.MeaningApi
{
    public class Definition
    {
        public Guid Id { get; set; }

        [JsonPropertyName("definition")]
        public string? DefinitionText { get; set; }
        [JsonPropertyName("example")]
        public string? Example { get; set; }
        [JsonPropertyName("antonyms")]
        public List<string>? Antonyms { get;    set; }
        [JsonPropertyName("synonyms")]
        public List<string>? Synonyms { get; set; }
    }
}
