using DictionaryApi.Models;
//using DictionaryApi.Models.DTOs;
using DictionaryApp.Helpers;
using DictionaryApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Refit;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace DictionaryApp.Controllers
{
    [AutoValidateAntiforgeryToken]
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class HomeController : Controller
    {
        private readonly IDictionaryApi dictionary;

        public HomeController(IDictionaryApi dictionary)
        {
            this.dictionary = dictionary;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Search(Word queryWord)
        {
            var wordDetails = await ExceptionHelper.ManageExceptions<BasicWordDetails>
               (async () => { return await dictionary.GetWordDetails(queryWord.word); }, TempData);
            if (wordDetails != null)
            {
                return View(wordDetails);
            }
            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> SearchById(Guid wordId)
        {
            var wordDetails = await dictionary.GetWordDetailsById(wordId);
            return View(nameof(Search), wordDetails);
        }
        
        [HttpGet]
        public async Task<IActionResult> Antonyms(Guid wordId)
        {
            var antonyms = await dictionary.GetAntonyms(wordId);
            return View(antonyms);
        }

        [HttpGet]
        public async Task<IActionResult> Synonyms(Guid wordId)
        {
            var synonyms = await dictionary.GetSynonyms(wordId);
            return View(synonyms);
        }
        [HttpGet]
        public async Task<IActionResult> Definition(int index, Guid wordId)
        {
            var definition = await dictionary.GetDefinition(index, wordId);
            return View(definition);
        }
        [HttpGet]
        public async Task<IActionResult> Pronounciation(Guid wordId)
        {
            var pronounciation = await dictionary.GetPronounciation(wordId);
            return Redirect(pronounciation);
        }
        [HttpGet]
        public async Task<IActionResult> SearchByStringWord(string word)
        {
            var wordDetails = await ExceptionHelper.ManageExceptions<BasicWordDetails>
              (async () => { return await dictionary.GetWordDetails(word); }, TempData);
            if (wordDetails != null)
            {
                return View("Search", wordDetails);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}