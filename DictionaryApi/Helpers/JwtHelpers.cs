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
		public static string GenerateToken(JwtOptions jwtConfiguration, string userId)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.UTF8.GetBytes(jwtConfiguration.IssuerSigningKey);
			DateTime expireTime = DateTime.UtcNow.AddMinutes(60);
            var claims = GetClaims(jwtConfiguration,userId);
            var tokenDescriptor = new SecurityTokenDescriptor
			{
				Issuer = jwtConfiguration.Issuer,
				Audience = jwtConfiguration.Audience,
				Subject = new ClaimsIdentity(claims),
				Expires = expireTime,
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
			};
			var jwtToken = new JwtSecurityToken(
					issuer: jwtConfiguration.Issuer,
					audience: jwtConfiguration.Audience,
					claims: claims,
					expires: expireTime,
					signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
					);
			var jwt = new JwtSecurityTokenHandler().WriteToken(jwtToken);
			return jwt;
		}
		private static IEnumerable<Claim> GetClaims(JwtOptions jwtConfiguration,string userId)
		{
			var claims = new List<Claim>()
			{
				new(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new("UserId",userId)
			};
			return claims;
		}
	}
}
