using System.Data.Entity;
using SimpleMVC4.Models.Accounts;
using SimpleMVC4.Models.Countries;
using SimpleMVC4.Models.Files;

namespace SimpleMVC4.Context
{
    public class SimpleMvc4ContextForScaffolding : DbContext
    {
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<CountryModel> CountryModels { get; set; }
        public DbSet<FileModel> FileModels { get; set; }
    }
}