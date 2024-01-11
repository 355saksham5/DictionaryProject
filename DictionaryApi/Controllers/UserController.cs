using DictionaryApi.BusinessLayer.Services.IServices;
using DictionaryApi.Helpers;
using DictionaryApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

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

        public UserController(UserManager<IdentityUser> userManager, IJwtTokenService jwt)
		{
			this.userManager = userManager;
			this.result = new UserIdentityResult();
			this.jwt = jwt;
			this.logInResult = new LogInResult();
		}

		[HttpPost]
		public async Task<IActionResult> Register([FromBody]RegisterModel model)
		{
            var user = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email
            };
            var resultOrignal = await userManager.CreateAsync(user,model.Password);
            result.Succeeded = resultOrignal.Succeeded;
            result.Errors = resultOrignal.Errors;
			if(resultOrignal.Succeeded)
			{
				return CreatedAtAction(nameof(Register),result);
			}
			
			return BadRequest(result);
		}

		[HttpPost]
		public async Task<IActionResult> Login([FromBody] LoginModel model)
		{
			var user = await userManager.FindByEmailAsync(model.Email);
			if (user == null)
			{
				logInResult.UserConflict = true;
				return NotFound(logInResult);
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
			return Unauthorized(logInResult);
		}
	}
}
