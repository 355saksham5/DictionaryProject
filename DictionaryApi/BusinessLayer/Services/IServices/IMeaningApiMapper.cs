using DictionaryApi.Models.DTOs;
using DictionaryApi.Models.MeaningApi;

namespace DictionaryApi.BusinessLayer.Services.IServices
{
	public interface IMeaningApiMapper
	{
		public Task<BasicWordDetails> MapBasicWordDetails(IEnumerable<WordDetails> wordDetails);
		public Task<string> MapDefinitions(IEnumerable<Meaning> meanings);
		public Task<string> MapPhoneticAudios(IEnumerable<Phonetic> phonetics);

	}
}
