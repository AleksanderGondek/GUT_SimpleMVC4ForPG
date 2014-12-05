using System.Web.Mvc;
using System.Web.UI;

namespace SimpleMVC4.Controllers
{
    [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
    public class ValidationController : Controller
    {
        public JsonResult FileName(string FileName)
        {
            if (string.IsNullOrEmpty(FileName))
            {
                return Json("error message", JsonRequestBehavior.AllowGet);
            }
            if (FileName.Equals(@"Image") || FileName.Equals(@"NotImage") || FileName.Equals(@"Something"))
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("error message", JsonRequestBehavior.AllowGet);
            }
        }
    }
}
