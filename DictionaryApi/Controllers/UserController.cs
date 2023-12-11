using DictionaryApi.BusinessLayer.Services.IServices;
using DictionaryApi.Helpers;
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
	[ApiVersion(ConstantResources.apiVersion)]
	public class UserController : ControllerBase
	{
		private readonly UserManager<IdentityUser> userManager;
		private UserIdentityResult result;
        private LogInResult logInResult;
        private IJwtTokenService jwt;

        public UserController(UserManager<IdentityUser> userManager, UserIdentityResult result, IJwtTokenService jwt
			,LogInResult logInResult)
		{
			this.userManager = userManager;
			this.result = result;
			this.jwt = jwt;
			this.logInResult = logInResult;
		}

		[HttpPost]
		public async Task<IActionResult> Register([FromBody]IdentityUser user,[Required] string password)
		{
			var resultOrignal = await userManager.CreateAsync(user,password);
            result.Succeeded = resultOrignal.Succeeded;
            result.Errors = resultOrignal.Errors;
			if(resultOrignal.Succeeded)
			{
				return CreatedAtAction(nameof(Register),result);
			}
			return Ok(result);
		}

		[HttpPost]
		public async Task<IActionResult> Login([FromBody] LoginModel model)
		{
			var user = await userManager.FindByEmailAsync(model.Email);
			if (user == null)
			{
				logInResult.UserConflict = true;
				return Ok(logInResult);
			}
			var result = await userManager.CheckPasswordAsync(user, model.Password);
			if(result)
			{
				var userId = user.Id;
				var token = await jwt.CreateTokenAsync(userId);
				logInResult.jwt = token;
				return Ok(logInResult);
            }
			logInResult.PasswordFail = true;
			return Ok(logInResult);
		}
	}
}
