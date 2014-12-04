using System.Web.Mvc;
using SimpleMVC4.Models.Countries;
using SimpleMVC4.Context;
using SimpleMVC4.Repositories;

namespace SimpleMVC4.Controllers
{
    public class CountriesController : Controller
    {
        private readonly CountriesRepository _countiresRepository;

        public CountriesController(ISimpleMvc4Context databaseContext)
        {
            _countiresRepository = new CountriesRepository(databaseContext);
        }

        public ActionResult Index()
        {
            return View(_countiresRepository.All);
        }

        public ActionResult Details(int id = 0)
        {
            CountryModel countrymodel = _countiresRepository.Find(id);
            if (countrymodel == null)
            {
                return HttpNotFound();
            }
            return View(countrymodel);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CountryModel countrymodel)
        {
            if (ModelState.IsValid)
            {
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