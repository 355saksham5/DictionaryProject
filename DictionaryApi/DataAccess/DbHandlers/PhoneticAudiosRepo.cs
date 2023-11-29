using DictionaryApi.Data;
using DictionaryApi.DataAccess.DbHandlers.IDbHandlers;
using DictionaryApi.Models.DTOs;
using DictionaryApi.Models.MeaningApi;

namespace DictionaryApi.DataAccess.DbHandlers
{
    public class PhoneticAudiosRepo : IPhoneticAudiosRepo
    {
        private readonly AppDbContext context;

        public PhoneticAudiosRepo(AppDbContext context)
        {
            this.context = context;
        }

        public async Task AddPronounciation(PhoneticDto phoneticDto)
        {
            await context.PhoneticAudios.AddAsync(phoneticDto);
            context.SaveChanges();
        }

        public async Task DeletePronounciationById(Guid id)
        {
            var wordPronounciation = context.PhoneticAudios.Where(context => context.Id == id);
            if (wordPronounciation == null)
            {
                return;
            }
            context.Remove(wordPronounciation);
            context.SaveChanges();
        }

        public async Task<PhoneticDto> GetPronounciationByWordId(Guid basicWordDetailsId)
        {
            return context.PhoneticAudios.FirstOrDefault(context => context.BasicWordDetailsId == basicWordDetailsId);
        }

        public async Task<PhoneticDto> UpdatePronounciation(PhoneticDto phoneticDto)
        {
            throw new NotImplementedException();
        }
    }
}
