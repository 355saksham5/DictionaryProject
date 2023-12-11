using DictionaryApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DictionaryApi.Extensions
{
    public static class JwtExtensions
	{
		public static void AddJwtTokenServices(this IServiceCollection services, IConfiguration configuration)
		{
			var jwtConfig = new JwtOptions();
			configuration.Bind(JwtOptions.SectionName, jwtConfig);

			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters()
				{
					IssuerSigningKey =
						new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.IssuerSigningKey)),
					ValidIssuer = jwtConfig.Issuer,
					ValidAudience = jwtConfig.Audience,
					ValidateIssuer = true,
                    ValidateAudience = true,
					ValidateIssuerSigningKey = true,
					ValidateLifetime = true
				};
			});
		}
	}
}
