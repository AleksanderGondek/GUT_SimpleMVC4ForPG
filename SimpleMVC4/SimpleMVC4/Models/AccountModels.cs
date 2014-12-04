using System.Data.Entity;
using SimpleMVC4.Models.Accounts;

namespace SimpleMVC4.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
    }
}
