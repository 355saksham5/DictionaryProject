using DictionaryApi.BusinessLayer.Services.IServices;
using Microsoft.AspNetCore.Identity;

namespace DictionaryApi.Data
{
    public class DataSeeder
    {
        public UserManager<IdentityUser> userManager;
        public AppDbContext context;

        public DataSeeder(AppDbContext context,UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
            this.context = context;
        }
        public async Task Seed()
        {
            await userManager.CreateAsync(new IdentityUser
            {
                Id = "23daab02-a1ec-469b-af60-ee27174a4c79",
                UserName = "sak@gmail.com",
                Email = "sak@gmail.com",
                PasswordHash = "AQAAAAEAACcQAAAAEI+qqyHWGrUM6D4t8Uk0nCpiHzuUbQIjCvvEoT/3yBYLcnTKwIHeOXLnjSPmX6fNvA=="
            });
            context.BasicWordDetails.Add(new Models.DTOs.BasicWordDetails
            {
                Id = new Guid("61c557cb-f633-4f54-bc49-296da6a25761"),
                Word = "apple"
                
            });
            context.SaveChanges();
            context.Definitions.Add(new Models.DTOs.DefinitionDto
            {
                Id = Guid.NewGuid(),
                BasicWordDetailsId = new Guid("61c557cb-f633-4f54-bc49-296da6a25761"),
                DefinitionText = "definition of apple"

            }) ; 
            context.SaveChanges();
            context.PhoneticAudios.Add(new Models.DTOs.PhoneticDto
            {
                Id = Guid.NewGuid(),
                BasicWordDetailsId = new Guid("61c557cb-f633-4f54-bc49-296da6a25761"),
                PronounceLink = "pronounciation of apple"

            });
            context.SaveChanges();
            context.Antonyms.Add(new Models.DTOs.Antonyms
            {
                Id = Guid.NewGuid(),
                BasicWordDetailsId = new Guid("61c557cb-f633-4f54-bc49-296da6a25761"),
                Antonym = new  List<Models.DTOs.Words> { new Models.DTOs.Words { Id = Guid.NewGuid() , Word ="antonym of apple" } }
               
            });
            context.SaveChanges();
            context.Synonyms.Add(new Models.DTOs.Synonyms
            {
                Id = Guid.NewGuid(),
                BasicWordDetailsId = new Guid("61c557cb-f633-4f54-bc49-296da6a25761"),
                Synonym = new List<Models.DTOs.Words> { new Models.DTOs.Words { Id = Guid.NewGuid(), Word = "synonym of apple" } }

            });
            context.SaveChanges();
            context.UserCache.Add(new Models.UserCache.UserCache
            {
                Id = Guid.NewGuid(),
                UserId = new Guid("23daab02-a1ec-469b-af60-ee27174a4c79"),
                Cache = new Models.UserCache.CachedWord { Id = Guid.NewGuid() , Word ="apple"},
            });
            context.SaveChanges();

        }
    }
}
