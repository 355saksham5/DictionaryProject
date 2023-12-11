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
        public async Task<string> CreateTokenAsync(string id) 
        {
            var userId = id;
            var token = await JwtHelpers.GenerateTokenAsync(options, userId);
            return token;
        }

    }
}
