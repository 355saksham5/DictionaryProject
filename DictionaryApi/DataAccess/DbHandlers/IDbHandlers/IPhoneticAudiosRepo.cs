using DictionaryApi.Models.DTOs;

namespace DictionaryApi.DataAccess.DbHandlers.IDbHandlers
{
    public interface IPhoneticAudiosRepo
    {
        public Task<PhoneticDto> GetPronounciationByWordId(Guid basicWordDetailsId);

        public Task AddPronounciation(PhoneticDto phoneticDto);

        public Task DeletePronounciationById(Guid id);

        public Task<PhoneticDto> UpdatePronounciation(PhoneticDto phoneticDto);
    }
}
