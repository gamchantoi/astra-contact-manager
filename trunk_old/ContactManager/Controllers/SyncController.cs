using System.Web.Mvc;
using ContactManager.Helpers;
using ContactManager.Models;
using ContactManager.Intefaces;
using ContactManager.Models.Validation;

namespace ContactManager.Controllers
{
    public class SyncController : BaseController
    {
        private readonly ISynchronizationService _service;
        private readonly UserHelper _helper = new UserHelper();

        public SyncController()
        {
            _service = new SynchronizationService(new ModelStateWrapper(this.ModelState));
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
