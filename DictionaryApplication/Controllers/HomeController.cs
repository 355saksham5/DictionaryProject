using DictionaryApplication.Dataaccess;
using DictionaryApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DictionaryApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
		private readonly IDictionaryApi dictionary;

		public HomeController(ILogger<HomeController> logger , IDictionaryApi dictionary)
        {
            _logger = logger;
			this.dictionary = dictionary;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

		[HttpPost]
		public async Task<IActionResult> Search(string queryWord)
		{
			var wordDetails= await dictionary.GetWordDetails(queryWord);
			return View("Search", wordDetails);
		}

	}
}