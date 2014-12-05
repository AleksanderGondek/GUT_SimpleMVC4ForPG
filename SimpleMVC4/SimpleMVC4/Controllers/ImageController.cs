using System.Web.Mvc;
using SimpleMVC4.Context;
using SimpleMVC4.Repositories;

namespace SimpleMVC4.Controllers
{
    public class ImageController : AsyncController
    {
        private readonly FilesRepository _filesRepository;

        public ImageController(ISimpleMvc4Context databaseContext)
        {
            _filesRepository = new FilesRepository(databaseContext);
        }

        public ActionResult GetImage(int id)
        {
            var filemodel = _filesRepository.Find(id);
            if (filemodel == null)
            {
                return HttpNotFound();
            }

            return File(filemodel.FileBytes, filemodel.ContentType, filemodel.FileName);
        }

        public void IndexAsync(int id = 0)
        {
            AsyncManager.OutstandingOperations.Increment();
            var t = new System.Timers.Timer {AutoReset = false, Interval = 4000};
            t.Elapsed += (evt, args) =>
            {
                AsyncManager.Parameters["value"] = 321;
                AsyncManager.OutstandingOperations.Decrement();
            };
            t.Start();
        }

        public ActionResult IndexCompleted(int value = 0)
        {
            @ViewBag.Id = value;
            return View();
        }


        protected override void Dispose(bool disposing)
        {
            _filesRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}
