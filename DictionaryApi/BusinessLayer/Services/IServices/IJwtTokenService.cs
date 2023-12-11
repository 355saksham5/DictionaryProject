namespace DictionaryApi.BusinessLayer.Services.IServices
{
    public interface IJwtTokenService
    {
        public Task<string> CreateTokenAsync(string id);
    }
}
