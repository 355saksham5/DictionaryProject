using DictionaryApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace DictionaryApi.Extensions
{
    public static class WebApplicationExtensions
	{
		public static void AddJwtTokenServices(this IServiceCollection services, IConfiguration configuration)
		{
			var jwtConfig = new JwtOptions();
			configuration.Bind(JwtOptions.SectionName, jwtConfig);

			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
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
		public static void AddSwaggerGenWithAuthorize(this IServiceCollection services)
		{
			services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement{
                  {
                    new OpenApiSecurityScheme()
                  {
                     Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearer" }
                  },new string[] {"Bearer"}
				  }});
            });
        }
	}
}
