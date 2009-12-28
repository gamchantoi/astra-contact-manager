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
            ViewData["HostName"] = _context.GetCurrentHost().Address;
            return View();
        }

        [Authorize(Roles = "admin")]
        public ActionResult SyncFromHost()
        {
            _service.SyncFromHost();
            ViewData["HostName"] = _context.GetCurrentHost().Address;
            return View("Index");
        }

        [Authorize(Roles = "admin")]
        public ActionResult SyncToHost()
        {
            _service.SyncToHost();
            ViewData["HostName"] = _context.GetCurrentHost().Address;
            return View("Index");
        }

    }
}