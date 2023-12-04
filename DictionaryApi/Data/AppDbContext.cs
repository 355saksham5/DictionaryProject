using DictionaryApi.Models.DTOs;
using DictionaryApi.Models.UserCache;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace DictionaryApi.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<BasicWordDetails> BasicWordDetails { get; set; }
        public DbSet<DefinitionDto> Definitions { get; set; }
        public DbSet<PhoneticDto> PhoneticAudios { get; set; }
		public DbSet<Synonyms> Synonyms { get; set; }
		public DbSet<Antonyms> Antonyms { get; set; }
		public DbSet<UserCache> UserCache { get; set; }

		public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
