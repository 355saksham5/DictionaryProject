using DictionaryApi.BusinessLayer.Services.IServices;
using DictionaryApi.Helpers;
using DictionaryApi.Models;
using Microsoft.Extensions.Options;

namespace DictionaryApi.BusinessLayer.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly JwtOptions options;
        public JwtTokenService(IOptions<JwtOptions> options)
        {
            this.options = options.Value;
        }
        public async Task<string> CreateToken(string id) 
        {
            var userId = id;
            var token = JwtHelpers.GenerateToken(options, userId); //await
            return token;
        }

    }
}
