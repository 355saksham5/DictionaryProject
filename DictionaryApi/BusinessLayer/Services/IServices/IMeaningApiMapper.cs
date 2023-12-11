using DictionaryApi.Models.DTOs;
using DictionaryApi.Models.MeaningApi;

namespace DictionaryApi.BusinessLayer.Services.IServices
{
	public interface IMeaningApiMapper
	{
		public Task<BasicWordDetails> MapBasicWordDetailsAsync(IEnumerable<WordDetails> wordDetails);
		public Task<string> MapDefinitionsAsync(IEnumerable<Meaning> meanings);
		public Task<string> MapPhoneticAudiosAsync(IEnumerable<Phonetic> phonetics);

	}
}
