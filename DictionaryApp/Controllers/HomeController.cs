using DictionaryApi.Models.DTOs;
using DictionaryApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Refit;
using System.Diagnostics;

namespace DictionaryApp.Controllers
{
	[AutoValidateAntiforgeryToken]
	public class HomeController : Controller
	{
		private readonly IDictionaryApi dictionary;

		public HomeController( IDictionaryApi dictionary)
		{
			this.dictionary = dictionary;
		}

		[HttpGet]
		public async Task<IActionResult> Default()
		{
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> Search(Word queryWord)
		{
			try
			{
                var wordDetails = await dictionary.GetWordDetails(queryWord.word);
                return View(wordDetails);
            }
			catch (ApiException ex)
			{
				return View("WordNotFound");
			}
			
		}
        [HttpGet]
        public async Task<IActionResult> SearchById(Guid wordId)
        {
            var wordDetails = await dictionary.GetWordDetailsById(wordId);
            return View(nameof(Search),wordDetails);
        }
        [HttpGet]
		public async Task<IActionResult> Antonyms(Guid wordId)
		{
			var antonyms= await dictionary.GetAntonyms(wordId);
			return View(antonyms);
		}
        
        [HttpGet]
		public async Task<IActionResult> Synonyms(Guid wordId)
		{
			var synonyms = await dictionary.GetSynonyms(wordId);
			return View(synonyms);
        }
		[HttpGet]
		public async Task<IActionResult> Definition(int index , Guid wordId)
		{
			var definition = await dictionary.GetDefinition(index,wordId);
			return View(definition);
		}
		[HttpGet]
		public async Task<IActionResult> Pronounciation(Guid wordId)
		{
			var pronounciation = await dictionary.GetPronounciation(wordId);
			return Redirect(pronounciation);
		}
	}
}