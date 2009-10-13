using System.Web.Mvc;
using ContactManager.Controllers;
using ContactManager.Helpers;
using ContactManager.Models.Validation;
using ContactManager.Intefaces;
using ContactManager.Models;

namespace ContactManager.Controllers
{
    [HandleError]
    public class HomeController : BaseController
    {
        private IContactService _service;

        public HomeController() 
        {
            _service = new ContactService(new ModelStateWrapper(this.ModelState));
        }

        public HomeController(IContactService service) 
        {
            _service = service;
        }

        [Authorize]
        public ActionResult Index()
        {
            return View(_service.GetContact(CurrentUserId));
        }

        public ActionResult About()
        {
            return View();
        }
    }
}