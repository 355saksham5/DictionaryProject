using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DictionaryApp.Controllers
{
	[AutoValidateAntiforgeryToken]
    [Authorize]
    public class HistoryController : Controller
    {
        private readonly IDictionaryApi dictionary;
		public HistoryController(IDictionaryApi dictionary)
        {
            this.dictionary = dictionary;
        }

        [HttpGet]
        public async Task<IActionResult> PastSearches()
        {
            var userHistory = await dictionary.GetCache();
            return View(userHistory);
        }
        [HttpPost]
        public async Task<IActionResult> ClearCache()
        {
            await dictionary.ClearHistory();
            return RedirectToAction(nameof(PastSearches));
        }
    }
}
