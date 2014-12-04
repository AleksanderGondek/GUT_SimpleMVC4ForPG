using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using SimpleMVC4.Models.Countries;
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
            AddCountries(context);
            AttachAllCountiresToUk(context);
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

        private void AddCountries(SimpleMvc4Context context)
        {
            if (context.CountryModels.Any())
            {
                return;
            }

            context.CountryModels.Add(new CountryModel
            {
                Name = @"United Kingdom of Great Britain and Northern Ireland",
                TotalArea = 243610,
                OfficialLanguage = @"English"
            });

            context.CountryModels.Add(new CountryModel
            {
                Name = @"Republic of Ireland",
                TotalArea = 70273,
                OfficialLanguage = @"Irish, English"
            });

            context.CountryModels.Add(new CountryModel
            {
                Name = @"French Republic",
                TotalArea = 640679,
                OfficialLanguage = @"French"
            });

            context.SaveChanges();
        }

        private void AttachAllCountiresToUk(SimpleMvc4Context context)
        {
            var uk = context.CountryModels.Single(x => x.Name.Equals("United Kingdom of Great Britain and Northern Ireland"));
            if (uk.CountryModels == null)
            {
                uk.CountryModels = new List<CountryModel>();
            }
            else
            {
                uk.CountryModels.Clear();
            }

            foreach (var country in context.CountryModels.Where(x => !x.Name.Equals("United Kingdom of Great Britain and Northern Ireland")))
            {
                uk.CountryModels.Add(country);
            }

            context.SaveChanges();
        }
    }
}