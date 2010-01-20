using System.Web.Mvc;
using ContactManager.Models.Validation;
using ContactManager.Web.Services;

namespace ContactManager.Web.Controllers
{
    public class SettingsController : Controller
    {
        private readonly IValidationDictionary validationDictionary;
        private readonly SettingsService _settingsServices;

        public SettingsController()
        {
            validationDictionary = new ModelStateWrapper(ModelState);
            _settingsServices = new SettingsService(validationDictionary);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ClearDB()
        {
            if(_settingsServices.ClearDB())
                return RedirectToAction("Index");
            return View("Index");
        }

    }
}
