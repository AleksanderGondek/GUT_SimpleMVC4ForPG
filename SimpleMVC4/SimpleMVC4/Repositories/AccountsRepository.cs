using SimpleMVC4.Context;
using SimpleMVC4.Models.Accounts;

namespace SimpleMVC4.Repositories
{
    public class AccountsRepository : BaseRepository<UserProfile>
    {
        public AccountsRepository(ISimpleMvc4Context databaseContext) : base(databaseContext){}

        public override UserProfile Find(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}