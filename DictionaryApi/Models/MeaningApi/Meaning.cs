using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace DictionaryApi.Models.MeaningApi
{
    public class Meaning
    {
        public Guid Id { get; set; }
        [JsonPropertyName("definitions")]
        public List<Definition>? Definitions { get; set; }
        [JsonPropertyName("partOfSpeech")]
        public string? PartOfSpeech { get; set; }

         [JsonPropertyName("antonyms")]
        public List<string>? Antonyms { get; set; }
		[JsonPropertyName("synonyms")]
		public List<string>? Synonyms { get; set; }

	}
}
