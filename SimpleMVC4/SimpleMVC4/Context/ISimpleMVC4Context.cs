using System;
using System.Linq;
using SimpleMVC4.Models.Accounts;
using SimpleMVC4.Models.Countries;
using SimpleMVC4.Models.Files;

namespace SimpleMVC4.Context
{
    public interface ISimpleMvc4Context : IDisposable
    {
        IQueryable<UserProfile> UserProfiles { get; }
        IQueryable<CountryModel> CountryModels { get; }
        IQueryable<FileModel> FileModels { get; }

        T Attach<T>(T entity) where T : class;
        T Add<T>(T entity) where T : class;
        T Delete<T>(T entity) where T : class;
        IQueryable<T> All<T>() where T : class;

        int SaveChanges();
    }
}