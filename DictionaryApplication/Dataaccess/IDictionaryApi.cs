using DictionaryApplication.Models;
using Refit;

namespace DictionaryApplication.Dataaccess
{
	public interface IDictionaryApi
	{
		 [Get("/BasicDetails/{queryWord}")]
		 Task<BasicWordDetails> GetWordDetails(string queryWord);
		
	}
}
