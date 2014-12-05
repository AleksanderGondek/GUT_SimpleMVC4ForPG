using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleMVC4.Helpers;
using SimpleMVC4.Models.Countries;
using SimpleMVC4.Context;
using SimpleMVC4.Repositories;

namespace SimpleMVC4.Controllers
{
    public class CountriesController : AsyncController
    {
        private readonly CountriesRepository _countiresRepository;

        public CountriesController(ISimpleMvc4Context databaseContext)
        {
            _countiresRepository = new CountriesRepository(databaseContext);
        }

        public void IndexAsync(int id = 0)
        {
            AsyncManager.OutstandingOperations.Increment();
            var t = new System.Timers.Timer { AutoReset = false, Interval = 4000 };
            t.Elapsed += (evt, args) => AsyncManager.OutstandingOperations.Decrement();
            t.Start();
        }

        public ActionResult IndexCompleted(int value = 0)
        {
            return View(_countiresRepository.All);
        }
        
        //public ActionResult Index()
        //{
        //    return View(_countiresRepository.All);
        //}

        public ActionResult Details(int id = 0)
        {
            CountryModel countrymodel = _countiresRepository.Find(id);
            if (countrymodel == null)
            {
                return HttpNotFound();
            }

            countrymodel.AllCountries = _countiresRepository.GetAllSelectableCountries();
            countrymodel.SelectedCountries = countrymodel.CountryModels.Select(x => x.CountryId.ToString()).ToArray();

            return View(countrymodel);
        }

        public ActionResult Create()
        {
            ViewBag.AllCountries = _countiresRepository.GetAllSelectableCountries();
            return View();
        }

        public ActionResult GetImage(int id)
        {
            var filemodel = _countiresRepository.Find(id);
            if (filemodel == null)
            {
                return HttpNotFound();
            }

            return File(filemodel.FileBytes, filemodel.ContentType, filemodel.FileName);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CountryModel countrymodel, HttpPostedFileBase fileUpload)
        {
            if (@"Slask".Equals(countrymodel.Name))
            {
                ModelState.AddModelError("Name", "Slask is not a country");
            }
            if (ModelState.IsValid && fileUpload != null)
            {
                countrymodel.FileName = fileUpload.FileName;
                countrymodel.FileBytes = FilesControllerHelper.ConvertUploadedFileToBytes(fileUpload);
                countrymodel.ContentType = fileUpload.ContentType;

                _countiresRepository.Save(countrymodel);
                return RedirectToAction("Index");
            }

            return View(countrymodel);
        }

        public ActionResult Edit(int id = 0)
        {
            CountryModel countrymodel = _countiresRepository.Find(id);
            if (countrymodel == null)
            {
                return HttpNotFound();
            }
            countrymodel.AllCountries = _countiresRepository.GetAllSelectableCountries();
            countrymodel.SelectedCountries = countrymodel.CountryModels.Select(x => x.CountryId.ToString()).ToArray();
            return View(countrymodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CountryModel countrymodel)
        {
            if (ModelState.IsValid)
            {
                _countiresRepository.Update(countrymodel);
                return RedirectToAction("Index");
            }
            return View(countrymodel);
        }

        public ActionResult Delete(int id = 0)
        {
            CountryModel countrymodel = _countiresRepository.Find(id);
            if (countrymodel == null)
            {
                return HttpNotFound();
            }
            return View(countrymodel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CountryModel countrymodel = _countiresRepository.Find(id);
            _countiresRepository.Remove(countrymodel);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _countiresRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}