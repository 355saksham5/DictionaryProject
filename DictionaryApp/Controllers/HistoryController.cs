using Microsoft.AspNetCore.Mvc;

namespace DictionaryApp.Controllers
{
    public class HistoryController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDictionaryApi dictionary;

        public HistoryController(ILogger<HomeController> logger, IDictionaryApi dictionary)
        {
            _logger = logger;
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
            return RedirectToAction("PastSearches");
        }
    }
}
