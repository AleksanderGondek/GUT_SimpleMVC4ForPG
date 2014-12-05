using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using SimpleMVC4.Models.Accounts;
using SimpleMVC4.Models.Countries;
using SimpleMVC4.Models.Files;

namespace SimpleMVC4.Context
{
    public class SimpleMvc4Context : DbContext, ISimpleMvc4Context
    {
        public SimpleMvc4Context() : base("DefaultConnection") { }
        public SimpleMvc4Context(string connectionString) : base(connectionString) { }
        public SimpleMvc4Context(DbConnection existingConnection, bool ifOwnsConnection) : base(existingConnection, ifOwnsConnection) { }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<CountryModel> CountryModels { get; set; }
        public DbSet<FileModel> FileModels { get; set; }

        IQueryable<UserProfile> ISimpleMvc4Context.UserProfiles { get { return UserProfiles.AsQueryable(); } }
        IQueryable<CountryModel> ISimpleMvc4Context.CountryModels { get { return CountryModels.AsQueryable(); } }
        IQueryable<FileModel> ISimpleMvc4Context.FileModels { get { return FileModels.AsQueryable(); } } 

        public T Attach<T>(T entity) where T : class
        {
            Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public T Add<T>(T entity) where T : class
        {
            return Set<T>().Add(entity);
        }

        public T Delete<T>(T entity) where T : class
        {
            return Set<T>().Remove(entity);
        }

        public IQueryable<T> All<T>() where T : class
        {
            return Set<T>().AsQueryable();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CountryModel>().HasMany(m => m.CountryModels).WithMany();
        } 
    }
}