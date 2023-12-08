using DictionaryApi.BusinessLayer.Services.IServices;
using DictionaryApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DictionaryApi.Controllers
{
	[AllowAnonymous]
	[ApiController]
	[Route("api/[controller]/[action]")]
	[ApiVersion("1.0")]
	public class UserController : Controller
	{
		private readonly UserManager<IdentityUser> userManager;
		private UserIdentityResult result;
        private IJwtTokenService jwt;

        public UserController(UserManager<IdentityUser> userManager, UserIdentityResult result, IJwtTokenService jwt)
		{
			this.userManager = userManager;
			this.result = result;
			this.jwt = jwt;
		}

		[HttpPost]
		public async Task<UserIdentityResult> Register([FromBody]IdentityUser user,[Required] string password)
		{
			var resultOrignal = await userManager.CreateAsync(user,password);
            result.Succeeded = resultOrignal.Succeeded;
            result.Errors = resultOrignal.Errors;
			return result;
		}

		[HttpPost]
		public async Task<string?> Login([FromBody] LoginModel model)
		{
			var user = await userManager.FindByEmailAsync(model.Email);
			var result = await userManager.CheckPasswordAsync(user, model.Password);
			if(result)
			{
				var userId = user.Id;
				var token = await jwt.CreateToken(userId);
				return token;
            }
			return null;  // add correct code
		}
	}
}
