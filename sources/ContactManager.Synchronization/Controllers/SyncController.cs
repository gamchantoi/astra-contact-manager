using System.Web.Mvc;
using ContactManager.Models;
using ContactManager.Models.Validation;
using ContactManager.Synchronization.Interfaces;
using ContactManager.Synchronization.Services;

namespace ContactManager.Synchronization.Controllers
{
    public class SyncController : Controller
    {
        private readonly ISynchronizationService _service;
        private readonly CurrentContext _context;

        public SyncController()
        {
            _service = new SynchronizationService(new ModelStateWrapper(ModelState));
            _context = new CurrentContext();
        }

        public ActionResult Index()
        {

            return PrepareIndex();
        }

        [Authorize(Roles = "admin")]
        public ActionResult SyncFromHost()
        {
            _service.SyncFromHost();
            ViewData["HostName"] = _context.GetCurrentHost().Address;
            return PrepareIndex();
        }

        [Authorize(Roles = "admin")]
        public ActionResult SyncToHost()
        {
            _service.SyncToHost();
            ViewData["HostName"] = _context.GetCurrentHost().Address;
            return PrepareIndex();
        }

        private ActionResult PrepareIndex()
        {
            var str = string.Empty;
            if (!ModelState.IsValid)
            {
                foreach (var item in ModelState.Values)
                    foreach (var error in item.Errors)
                        str += error.ErrorMessage;
                TempData["Message"] = str;
            }
            else
                TempData["Message"] = "Synchronization success.";
            return RedirectToAction("Index", "Settings", new { area = ""});
        }

    }
}