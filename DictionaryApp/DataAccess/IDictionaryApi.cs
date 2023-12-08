using DictionaryApi.Models;
using DictionaryApi.Models.UserCache;
using DictionaryApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Refit;

[Headers("Authorization: Bearer")]
public interface IDictionaryApi
{
	#region WordMeaning
	[Get("/api/Word/BasicDetails")]
	Task<BasicWordDetails> GetWordDetails(string queryWord);
    [Get("/api/Word/BasicDetailsById")]
    Task<BasicWordDetails> GetWordDetailsById(Guid wordId);
    [Get("/api/Word/Pronounciation")]
	Task<string> GetPronounciation(Guid wordId);
	[Get("/api/Word/Antonyms")]
	Task<IEnumerable<string>> GetAntonyms(Guid wordId);
	[Get("/api/Word/Synonyms")]
	Task<IEnumerable<string>> GetSynonyms(Guid wordId);
	[Get("/api/Word/Definition")]
	Task<Definition> GetDefinition(int index, Guid wordId);
	#endregion


	#region  User
	[Post("/api/User/Login")]
    Task<string> LogIn(LoginModel model);
    [Post("/api/User/Register")]
    Task<UserIdentityResult> Register(IdentityUser user, string password);
	#endregion

	#region History
	[Post("/api/History/ClearCache")]
    Task ClearHistory();
    [Get("/api/History")]
    Task<IEnumerable<CachedWord>> GetCache();
	#endregion

}