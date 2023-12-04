using DictionaryApi.Migrations;
using DictionaryApi.Models;
using DictionaryApp.Models;
using DictionaryApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DictionaryApp.Controllers
{
	public class AccountController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IDictionaryApi dictionary;

		public AccountController(ILogger<HomeController> logger, IDictionaryApi dictionary)
		{
			_logger = logger;
			this.dictionary = dictionary;
		}

		[HttpGet]
		public async Task<IActionResult> Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };
				var result = await dictionary.Register(user,model.Password);
				if (result.Succeeded)
                {
                   return RedirectToAction("default", "home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
			return View(model);
        }
		[HttpGet]
		public async Task<IActionResult> LogIn()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> LogIn(LogInViewModel model)
		{
            if (ModelState.IsValid)
            {
                var logInCred = new LoginModel
                {
                    Password = model.Password,
                    Email = model.Email
                };
                var result = await dictionary.LogIn(logInCred);

                if (result!=null)
                {
                    HttpContext.Response.Cookies.Append("Authorization",result, new CookieOptions { HttpOnly = true });
                    return RedirectToAction("default", "home");
                }
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }
            return View(model);
        }

	}
}
