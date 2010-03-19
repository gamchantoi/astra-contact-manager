using System.Web.Mvc;
using ContactManager.Models;
using ContactManager.Models.Validation;
using ContactManager.Users.Interfaces;
using ContactManager.Users.Services;

namespace ContactManager.Users.Controllers
{
    public class StatusController : Controller
    {
        private readonly IStatusService _statusService;

        public StatusController()
        {
            _statusService = new StatusService(new ModelStateWrapper(ModelState));
        }

        public ActionResult Index()
        {
            return View(_statusService.ListStatuses());
        }

        public ActionResult Edit(int id)
        {
            return View(_statusService.GetStatus(id));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(Status status)
        {
            try
            {
                _statusService.EditStatus(status);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(status);
            }
        }

    }
}
