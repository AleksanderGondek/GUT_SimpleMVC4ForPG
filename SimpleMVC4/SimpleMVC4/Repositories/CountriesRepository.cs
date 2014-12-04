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
                return base.Save(entity);
            }

            return false;
        }

        public override void Update(CountryModel entity)
        {
            DatabaseContext.Attach(entity);
            DatabaseContext.SaveChanges();
            
            var ids = entity.SelectedCountries.Select(int.Parse).ToList();
            if (ids.Any())
            {
                if (entity.CountryModels == null)
                {
                    entity.CountryModels = new List<CountryModel>();
                }
                entity.CountryModels.Clear();
                foreach (var id in ids)
                {
                   entity.CountryModels.Add(DatabaseContext.CountryModels.Single(x => x.CountryId.Equals(id)));
                }

                DatabaseContext.Attach(entity);
                DatabaseContext.SaveChanges();
            }

        }

        public CountryModel AddCountriesToModel(CountryModel model)
        {
            var ids = model.SelectedCountries.Select(int.Parse).ToList();
            var countriesToBeAdded = DatabaseContext.CountryModels.Where(x => ids.Contains(x.CountryId));

            if (model.CountryModels != null)
            {
                model.CountryModels.Clear();
            }
            else
            {
                model.CountryModels = new List<CountryModel>();
            }

            foreach (var country in countriesToBeAdded)
            {
                model.CountryModels.Add(country);
            }

            return model;
        }

        public IEnumerable<SelectListItem> GetAllSelectableCountries()
        {
            return All.Select(countryModel =>
                new SelectListItem { Value = countryModel.CountryId.ToString(), Text = countryModel.Name, /*Selected = model.CountryModels.Any(z => z.CountryId.Equals(countryModel.CountryId))*/ })
                .AsEnumerable();
        }
    }
}