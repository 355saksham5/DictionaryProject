using DictionaryApi.BusinessLayer.Services.IServices;
using DictionaryApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DictionaryApi.Controllers
{
	[AllowAnonymous]
	[ApiController]
	[Route("[action]")]
	public class UserController : Controller
	{
		private readonly UserManager<IdentityUser> userManager;
		private readonly SignInManager<IdentityUser> signInManager;
		private UserIdentityResult result;
        private IJwtTokenService jwt;

        public UserController(UserManager<IdentityUser> userManager,
			SignInManager<IdentityUser> signInManager, UserIdentityResult result, IJwtTokenService jwt)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
			this.result = result;
			this.jwt = jwt;
		}


		[HttpPost]
		public async Task Logout()
		{
			await signInManager.SignOutAsync();
		}

		[HttpPost]
		public async Task<UserIdentityResult> Register([FromBody]IdentityUser user,[FromQuery] string password)
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
		public async Task<string?> Login([FromBody] LoginModel model)
		{
			var result = await signInManager.PasswordSignInAsync(
					model.Email, model.Password, model.RememberMe, false);
			if(result.Succeeded)
			{
				var userId = userManager.Users.FirstOrDefault(user=>user.UserName==model.Email).Id;
				var token = await jwt.CreateToken(userId);
				return token;
            }
			return null;  // add correct code
		}
	}
}
