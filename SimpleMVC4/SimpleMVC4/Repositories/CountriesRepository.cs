using System.Linq;
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
    }
}