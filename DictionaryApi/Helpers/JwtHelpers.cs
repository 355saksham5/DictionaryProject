using DictionaryApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DictionaryApi.Helpers
{
    public static class JwtHelpers
	{
		public static async Task<string> GenerateTokenAsync(JwtOptions jwtConfiguration, string userId)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.UTF8.GetBytes(jwtConfiguration.IssuerSigningKey);
			DateTime expireTime = DateTime.UtcNow.AddDays(1);
            var claims = await GetClaimsAsync(jwtConfiguration,userId);
            var tokenDescriptor = new SecurityTokenDescriptor
			{
				Issuer = jwtConfiguration.Issuer,
				Audience = jwtConfiguration.Audience,
				Subject = new ClaimsIdentity(claims),
				Expires = expireTime,
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
			};
			var jwtToken = tokenHandler.CreateToken(tokenDescriptor);
			var jwt = new JwtSecurityTokenHandler().WriteToken(jwtToken);
			return jwt;
		}
		private static async Task<IEnumerable<Claim>> GetClaimsAsync(JwtOptions jwtConfiguration,string userId)
		{
			var claims = new List<Claim>()
			{
				new(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new(ConstantResources.claimInJwt,userId)
			};
			return claims;
		}
	}
}
