using DictionaryApi.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DictionaryApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class AuthenticationController : ControllerBase
	{
		private readonly JwtOptions options;

		public AuthenticationController(IOptions<JwtOptions> options)
		{
			this.options = options.Value;
		}

		[HttpPost("token")]
		public async Task<IActionResult> CreateToken()
		{
			var id = Guid.NewGuid();
			var token = JwtHelpers.GenerateToken(options,id);
			return Ok(token);
		}
	}
}
