using System.Web.Mvc;
using ContactManager.Hosts.Helpers;
using ContactManager.Models.Validation;
using ContactManager.Synchronization.Interfaces;
using ContactManager.Synchronization.Models;

namespace ContactManager.Synchronization.Controllers
{
    public class SyncController : Controller
    {
        private readonly ISynchronizationService _service;
        private readonly HostHelper _helper = new HostHelper();

        public SyncController()
        {
            _service = new SynchronizationService(new ModelStateWrapper(ModelState));
        }

        public SyncController(ISynchronizationService service)
        {
            _service = service;
        }

        //
        // GET: /Sync/

        public ActionResult Index()
        {
            ViewData["HostName"] = _helper.GetCurrentHost().Address;
            return View();
        }

        [Authorize(Roles = "admin")]
        public ActionResult SyncFromHost()
        {
            _service.SyncFromHost();
            ViewData["HostName"] = _helper.GetCurrentHost().Address;
            return View("Index");
        }

        [Authorize(Roles = "admin")]
        public ActionResult SyncToHost()
        {
            _service.SyncToHost();
            ViewData["HostName"] = _helper.GetCurrentHost().Address;
            return View("Index");
        }

    }
}