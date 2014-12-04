using System;
using System.Collections.Generic;
using System.Linq;
using SimpleMVC4.Context;

namespace SimpleMVC4.Repositories
{
    public abstract class BaseRepository<T> where T : class
    {
        protected BaseRepository(ISimpleMvc4Context databaseContext)
        {
            DatabaseContext = databaseContext;
        }
        
        protected ISimpleMvc4Context DatabaseContext;
        
        public List<T> All
        {
            get
            {
                var all = new List<T>();
                try { all = DatabaseContext.All<T>().ToList(); }
                catch (Exception)
                { }
                return all;
            }
        }

        public IQueryable<T> Query
        {
            get { return DatabaseContext.All<T>(); }
        }

        public virtual void Update(T entity)
        {
            DatabaseContext.Attach(entity);
            DatabaseContext.SaveChanges();
        }

        public virtual bool Save(T entity)
        {
            DatabaseContext.Add(entity);
            DatabaseContext.SaveChanges();
            return true;
        }

        public virtual void Remove(T entity)
        {
            DatabaseContext.Delete(entity);
            DatabaseContext.SaveChanges();
        }

        public virtual void Dispose()
        {
            if (DatabaseContext == null) return;
            DatabaseContext.Dispose();
        }

        public void Add(T entity)
        {
            DatabaseContext.Add(entity);
        }

        public void Attach(T entity)
        {
            DatabaseContext.Attach(entity);
        }

        public abstract T Find(int id);
    }
}