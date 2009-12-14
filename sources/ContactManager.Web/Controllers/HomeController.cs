using System.Web.Mvc;
using ContactManager.Models.Validation;
using ContactManager.Users.Interfaces;
using ContactManager.Users.Services;
using ContactManager.Web.Helpers;

namespace ContactManager.Web.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        private readonly IUserFasade _service;

        public HomeController() 
        {
            _service = new UserFasade(new ModelStateWrapper(ModelState));
        }

        public HomeController(IUserFasade service) 
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