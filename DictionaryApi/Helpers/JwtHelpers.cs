using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DictionaryApi.Helpers
{
	public static class JwtHelpers
	{
		public static string GenerateToken(JwtOptions jwtConfiguration, Guid userId)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.UTF8.GetBytes(jwtConfiguration.IssuerSigningKey);
			DateTime expireTime = DateTime.UtcNow.AddDays(1);
			var claims = GetClaims(userId);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Issuer = jwtConfiguration.Issuer,
				Audience = jwtConfiguration.Audience,
				Subject = new ClaimsIdentity(claims),
				Expires = expireTime,
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
			};		

			var token = tokenHandler.CreateToken(tokenDescriptor);
			var jwt = tokenHandler.WriteToken(token);
			return jwt;
		}
		private static IEnumerable<Claim> GetClaims(Guid userId)
		{
			var claims = new List<Claim>()
			{
				new(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
				new("UserId",userId.ToString())
			};
			return claims;
		}
	}
}
