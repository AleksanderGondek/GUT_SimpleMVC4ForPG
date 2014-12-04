using System.Data.Entity.Migrations;
using WebMatrix.WebData;

namespace SimpleMVC4.Context
{
    public class DatabaseInitializator : DbMigrationsConfiguration<SimpleMvc4Context>
    {
        public DatabaseInitializator()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(SimpleMvc4Context context)
        {
            AttachSimpleAuth();
            AddUsers();
        }

        private void AttachSimpleAuth()
        {
            WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", autoCreateTables: true);
        }

        private void AddUsers()
        {
            if (!WebSecurity.UserExists("admin")) { WebSecurity.CreateUserAndAccount("admin", "admin"); }
            if (!WebSecurity.UserExists("user0")) { WebSecurity.CreateUserAndAccount("user0", "qwerty"); }
        }
    }
}