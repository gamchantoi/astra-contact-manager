using System.Web.Mvc;
using ContactManager.Models.Validation;
using ContactManager.Users.Interfaces;
using ContactManager.Users.Models;
using ContactManager.Web.Helpers;

namespace ContactManager.Web.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        private readonly IContactService _service;

        public HomeController() 
        {
            _service = new ContactService(new ModelStateWrapper(ModelState));
        }

        public HomeController(IContactService service) 
        {
            _service = service;
        }

        [Authorize]
        public ActionResult Index()
        {
            var helper = new UserHelper();
            return View(_service.GetContact(helper.CurrentUserId));
        }

        public ActionResult About()
        {
            return View();
        }
    }
}