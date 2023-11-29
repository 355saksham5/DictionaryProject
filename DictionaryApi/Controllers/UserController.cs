using DictionaryApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DictionaryApi.Controllers
{
	[ApiController]
	[Route("[action]")]
	public class UserController : Controller
	{
		private readonly UserManager<IdentityUser> userManager;
		private readonly SignInManager<IdentityUser> signInManager;
		private UserIdentityResult result;

		public UserController(UserManager<IdentityUser> userManager,
			SignInManager<IdentityUser> signInManager, UserIdentityResult result)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
			this.result = result;
		}


		[HttpPost]
		public async Task Logout()
		{
			await signInManager.SignOutAsync();
		}

		[HttpPost]
		public async Task<UserIdentityResult> Register(IdentityUser user, string password)
		{
			var resultOrignal = await userManager.CreateAsync(user,password);
            result.Succeeded = resultOrignal.Succeeded;
            result.Errors = resultOrignal.Errors;
            if (result.Succeeded)
			{
				await signInManager.SignInAsync(user, isPersistent: false);
			}
			return result;
		}

		[HttpPost]
		public async Task<bool> Login(LoginModel model)
		{
			var resultOrignal = await signInManager.PasswordSignInAsync(
					model.Email, model.Password, model.RememberMe, false);
            return resultOrignal.Succeeded;
		}
	}
}
