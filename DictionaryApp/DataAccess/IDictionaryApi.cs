using DictionaryApi.Models;
using DictionaryApp.Models;
using Microsoft.AspNetCore.Identity;
using Refit;

public interface IDictionaryApi
{
	[Get("/BasicDetails/{queryWord}")]
	Task<BasicWordDetails> GetWordDetails(string queryWord);
	[Get("/Pronounciation/{wordId}")]
	Task<string> GetPronounciation(Guid wordId);
	[Get("/Antonym/{wordId}")]
	Task<IEnumerable<string>> GetAntonyms(Guid wordId);
	[Get("/Synonym/{wordId}")]
	Task<IEnumerable<string>> GetSynonyms(Guid wordId);
	[Get("/Definition/{index}&{wordId}")]
	Task<Definition> GetDefinition(int index, Guid wordId);
    [Post("/Logout")]
    Task LogOut();
    [Post("/Login")]
    Task<bool> LogIn(LoginModel model);
    [Post("/Register")]
    Task<UserIdentityResult> Register(IdentityUser user, string password);

}