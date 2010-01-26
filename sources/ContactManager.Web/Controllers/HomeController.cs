using System.Web.Mvc;
using ContactManager.Models;
using ContactManager.Models.Validation;
using ContactManager.Users.Interfaces;
using ContactManager.Users.Services;
using ContactManager.Users.ViewModels;
using ContactManager.Web.Helpers;

namespace ContactManager.Web.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        private readonly IUserFasade _facade;
        private readonly CurrentContext _ctx;

        public HomeController() 
        {
            _facade = new UserFasade(new ModelStateWrapper(ModelState));
            _ctx = new CurrentContext();
        }

        [Authorize]
        public ActionResult Index()
        {
            var client = _facade.ClientService.GetClient(_ctx.CurrentUserId);
            ClientViewModel viewModel;
            
            viewModel = _facade.ClientService.GetViewModel(client);
            //viewModel.Profiles = pppHelper.GetProfiles(client.ProfileId);
            //viewModel.Statuses = _statusService.ListStatuses(2);
            return View(viewModel);
        }

        public ActionResult About()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        public ActionResult CleanDatabase()
        {
            var helper = new UserHelper();
            helper.CleanDatabase();
            return View("Index", _facade.GetContact(_ctx.CurrentUserId));
        }
    }
}