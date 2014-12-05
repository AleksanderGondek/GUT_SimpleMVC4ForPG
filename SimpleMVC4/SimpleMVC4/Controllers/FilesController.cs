using System.Web;
using System.Web.Mvc;
using SimpleMVC4.Helpers;
using SimpleMVC4.Models.Files;
using SimpleMVC4.Context;
using SimpleMVC4.Repositories;

namespace SimpleMVC4.Controllers
{
    public class FilesController : Controller
    {
        private readonly FilesRepository _filesRepository;

        public FilesController(ISimpleMvc4Context databaseContext)
        {
            _filesRepository = new FilesRepository(databaseContext);
        }

        public ActionResult Index()
        {
            return View(_filesRepository.All);
        }

        public ActionResult Details(int id = 0)
        {
            var filemodel = _filesRepository.Find(id);
            if (filemodel == null)
            {
                return HttpNotFound();
            }
            return View(filemodel);
        }

        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(FileModel filemodel, HttpPostedFileBase fileUpload)
        {
            if (ModelState.IsValid && fileUpload != null)
            {
                filemodel.FileBytes = FilesControllerHelper.ConvertUploadedFileToBytes(fileUpload);
                filemodel.ContentType = fileUpload.ContentType;
                _filesRepository.Save(filemodel);
                return RedirectToAction("Index");
            }

            return View(filemodel);
        }

        public ActionResult Download(int id = 0)
        {
            var filemodel = _filesRepository.Find(id);
            if (filemodel == null)
            {
                return HttpNotFound();
            }

            return File(filemodel.FileBytes, filemodel.ContentType, filemodel.FileName);
        }

        public ActionResult Delete(int id = 0)
        {
            FileModel filemodel = _filesRepository.Find(id);
            if (filemodel == null)
            {
                return HttpNotFound();
            }
            return View(filemodel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var filemodel = _filesRepository.Find(id);
            _filesRepository.Remove(filemodel);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _filesRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}