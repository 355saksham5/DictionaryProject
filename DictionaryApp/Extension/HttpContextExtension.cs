using Azure;
using DictionaryApp.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Security.Principal;

namespace DictionaryApp.Extension
{
	public static class HttpContextExtension
	{
		public static async Task OnLogInAsync(this HttpContext context, string token)
		{
			context.Response.Cookies.Append(ConstantResources.cookieName, token, new CookieOptions
			{
				HttpOnly = true,
				Expires = DateTime.UtcNow.AddDays(ConstantResources.expiresInDays)
			});
			var claim = new ClaimsIdentity(new List<Claim> { new(ConstantResources.loginClaimKey, ConstantResources.loginClaimValue) }, CookieAuthenticationDefaults.AuthenticationScheme);
			var principalClaim = new ClaimsPrincipal(claim);
			context.User = principalClaim;
            await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principalClaim, new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTime.UtcNow.AddDays(1)
            });
        }
		public static async Task OnLogOutAsync(this HttpContext context)
		{
			context.Response.Cookies.Delete(ConstantResources.cookieName);
			context.User = new ClaimsPrincipal();
			context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
		}
	}
}
