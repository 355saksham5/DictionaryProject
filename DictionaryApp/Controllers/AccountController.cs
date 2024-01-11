using DictionaryApp.Helpers;
using DictionaryApi.Models;
using DictionaryApp.Extension;
using DictionaryApp.Models;
using DictionaryApp.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.Cookies;
using DictionaryApi.BusinessLayer.Services;
using System.Security.Claims;

namespace DictionaryApp.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class AccountController : Controller
	{
		private readonly IDictionaryApi dictionaryApi;

		public AccountController( IDictionaryApi dictionary)
		{
			this.dictionaryApi = dictionary;
		}

		[HttpGet]
        public async Task<IActionResult> Register()
		{
            if(User.Identity.IsAuthenticated)
            {
                return Redirect("~/");
            }
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel modelView)
		{
            UserIdentityResult? result;
            
            if (ModelState.IsValid)
            {
                var model = new RegisterModel
                {
                    Email = modelView.Email,
                    Password = modelView.Password,
                };
                result = await ExceptionHelper.ManageExceptionsRegister<UserIdentityResult?>
                            (async () => { return await dictionaryApi.Register(model); }, ModelState);
                if (result != null)
                {
                   return RedirectToAction(nameof(LogIn));
                }
               
            }
            return View(modelView);
        }
		[HttpGet]
		public async Task<IActionResult> LogIn()
		{
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("~/");
            }
            return View();
		}

		[HttpPost]
		public async Task<IActionResult> LogIn(LogInViewModel model)
		{
            if (ModelState.IsValid)
            {
                var logInCred = new LoginModel { Password = model.Password , Email = model.Email };
                var result = await ExceptionHelper.ManageExceptionsLogIn<LogInResult>
               (async () => { return await dictionaryApi.LogIn(logInCred); }, ModelState);
                if (result!=null)
                {
                    await HttpContext.OnLogInAsync(result.jwt);
                    return RedirectToAction("index", "home");
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
