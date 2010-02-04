using System.Web.Mvc;
using ContactManager.Models;
using ContactManager.Models.Validation;
using ContactManager.Web.Models;
using ContactManager.Web.ViewModels;

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

        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            var ctx = new CurrentContext();
            var model = new SettingsViewModel
                            {
                                BuildVersion = AssemblyVersion.Version,
                                ServerName = ctx.GetCurrentHost().Address
                            };
            return View(model);
        }

        [Authorize(Roles = "admin")]
        public ActionResult ClearDB()
        {
            if(_settingsServices.ClearDB())
                return RedirectToAction("Index");
            return View("Index");
        }
    }
}
