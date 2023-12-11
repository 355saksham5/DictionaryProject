using DictionaryApi.Models;
using DictionaryApp.Extension;
using DictionaryApp.Helpers;
using DictionaryApp.Models;
using DictionaryApp.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DictionaryApp.Controllers
{
	[AutoValidateAntiforgeryToken]
	public class AccountController : Controller
	{
		private readonly IDictionaryApi dictionary;

		public AccountController( IDictionaryApi dictionary)
		{
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
                   return RedirectToAction(nameof(LogIn));
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
                var logInCred = new LoginModel { Password = model.Password , Email = model.Email };
                var result = await dictionary.LogIn(logInCred);
                if (!String.IsNullOrEmpty(result.jwt))
                {
                    await HttpContext.OnLogInAsync(result.jwt);
                    return RedirectToAction("default", "home");
                }
                if(result.PasswordFail)
                {
                    ModelState.AddModelError(string.Empty, ConstantResources.wrongCredErr);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, ConstantResources.userNotFoundErr);
                }
                
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.OnLogOutAsync();
            return RedirectToAction(nameof(LogIn));
        }

    }
}
