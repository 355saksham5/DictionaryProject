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

        public async Task AddPronounciationAsync(PhoneticDto phoneticDto)
        {
            await context.PhoneticAudios.AddAsync(phoneticDto);
            await context.SaveChangesAsync();
        }

        public async Task DeletePronounciationByIdAsync(Guid id)
        {
            var wordPronounciation = context.PhoneticAudios.Where(context => context.BasicWordDetailsId == id);
            if (wordPronounciation == null)
            {
                return;
            }
            context.Remove(wordPronounciation);
            await context.SaveChangesAsync();
        }

        public async Task<PhoneticDto?> GetPronounciationByWordIdAsync(Guid basicWordDetailsId)
        {
            return context.PhoneticAudios.FirstOrDefault(context => context.BasicWordDetailsId == basicWordDetailsId);
        }

        
    }
}
