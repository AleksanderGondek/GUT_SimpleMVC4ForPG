using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SimpleMVC4.Context;
using SimpleMVC4.Models.Countries;

namespace SimpleMVC4.Repositories
{
    public class CountriesRepository : BaseRepository<CountryModel>
    {
        public CountriesRepository(ISimpleMvc4Context databaseContext) : base(databaseContext) {}

        public override CountryModel Find(int id)
        {
            return DatabaseContext.CountryModels.SingleOrDefault(x => x.CountryId.Equals(id));
        }

        public override bool Save(CountryModel entity)
        {
            if (!DatabaseContext.CountryModels.Any(x => x.Name.Equals(entity.Name)))
            {
                if (entity.SelectedCountries != null && entity.SelectedCountries.Any())
                {
                    var ids = entity.SelectedCountries.Select(int.Parse).ToList();
                    if (ids.Any())
                    {
                        entity.CountryModels = new List<CountryModel>();
                        foreach (var id in ids)
                        {
                            entity.CountryModels.Add(DatabaseContext.CountryModels.Single(x => x.CountryId.Equals(id)));
                        }
                    }
                }
            }
            return base.Save(entity);
        }

        public override void Update(CountryModel entity)
        {
            if (entity.SelectedCountries == null || !entity.SelectedCountries.Any())
            {
                var myEntitye = DatabaseContext.CountryModels.Single(x => x.CountryId.Equals(entity.CountryId));
                if (myEntitye.CountryModels == null)
                {
                    myEntitye.CountryModels = new List<CountryModel>();
                }
                myEntitye.CountryModels.Clear();
                DatabaseContext.Attach(myEntitye);
                DatabaseContext.SaveChanges();
                return;
            }

            var ids = entity.SelectedCountries.Select(int.Parse).ToList();
            if (!ids.Any()) return;

            var myEntity = DatabaseContext.CountryModels.Single(x => x.CountryId.Equals(entity.CountryId));
            if (myEntity.CountryModels == null)
            {
                myEntity.CountryModels = new List<CountryModel>();
            }
            myEntity.CountryModels.Clear();
            foreach (var id in ids)
            {
                myEntity.CountryModels.Add(DatabaseContext.CountryModels.Single(x => x.CountryId.Equals(id)));
            }

            DatabaseContext.Attach(myEntity);
            DatabaseContext.SaveChanges();
        }

        public IEnumerable<SelectListItem> GetAllSelectableCountries()
        {
            return All.Select(countryModel =>
                new SelectListItem { Value = countryModel.CountryId.ToString(), Text = countryModel.Name, /*Selected = model.CountryModels.Any(z => z.CountryId.Equals(countryModel.CountryId))*/ })
                .AsEnumerable();
        }
    }
}