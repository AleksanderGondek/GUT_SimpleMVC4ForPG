using System.Linq;
using SimpleMVC4.Context;
using SimpleMVC4.Models.Files;

namespace SimpleMVC4.Repositories
{
    public class FilesRepository : BaseRepository<FileModel>
    {
        public FilesRepository(ISimpleMvc4Context databaseContext) : base(databaseContext) { }


        public override FileModel Find(int id)
        {
            return DatabaseContext.FileModels.SingleOrDefault(x => x.FileId.Equals(id));
        }
    }
}