using DictionaryApi.Models.DTOs;

namespace DictionaryApi.DataAccess.DbHandlers.IDbHandlers
{
    public interface IPhoneticAudiosRepository
    {
        public Task<PhoneticDto> GetPronounciationByWordIdAsync(Guid basicWordDetailsId);

        public Task AddPronounciationAsync(PhoneticDto phoneticDto);

        public Task DeletePronounciationByIdAsync(Guid id);

    }
}
