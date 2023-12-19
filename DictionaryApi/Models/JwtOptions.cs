using DictionaryApi.Helpers;

namespace DictionaryApi.Models
{
    public class JwtOptions
    {
        public const string SectionName = ConstantResources.jwtSectionName;
        public string? IssuerSigningKey { get; set; }
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
    }
}
