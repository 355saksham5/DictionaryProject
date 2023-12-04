namespace DictionaryApi.Models
{
    public class JwtOptions
    {
        public const string SectionName = "JwtConfiguration";
        public string IssuerSigningKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
