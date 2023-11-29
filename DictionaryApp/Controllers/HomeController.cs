using DictionaryApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DictionaryApp.Controllers
{
	[Authorize]
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IDictionaryApi dictionary;

		public HomeController(ILogger<HomeController> logger, IDictionaryApi dictionary)
		{
			_logger = logger;
			this.dictionary = dictionary;
		}

		[AllowAnonymous]
		[HttpGet]
		public async Task<IActionResult> Default()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Search(Word queryWord)
		{
			var wordDetails = await dictionary.GetWordDetails(queryWord.word);
			return View(wordDetails);
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
			return View(pronounciation);
		}
	}
}