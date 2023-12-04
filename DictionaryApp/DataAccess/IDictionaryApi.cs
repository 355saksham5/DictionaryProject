using DictionaryApi.Models;
using DictionaryApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Refit;

[Headers("Authorization: Bearer")]
public interface IDictionaryApi
{
	[Get("/BasicDetails/{queryWord}")]
	Task<BasicWordDetails> GetWordDetails(string queryWord);
	[Get("/Pronounciation/{wordId}")]
	Task<string> GetPronounciation(Guid wordId);
	[Get("/Antonyms/{wordId}")]
	Task<IEnumerable<string>> GetAntonyms(Guid wordId);
	[Get("/Synonyms/{wordId}")]
	Task<IEnumerable<string>> GetSynonyms(Guid wordId);
	[Get("/Definition/{index}&{wordId}")]
	Task<Definition> GetDefinition(int index, Guid wordId);
    [Post("/Logout")]
    Task LogOut();
    [Post("/Login")]
    Task<string> LogIn(LoginModel model);
    [Post("/Register")]
    Task<UserIdentityResult> Register(IdentityUser user, string password);

}