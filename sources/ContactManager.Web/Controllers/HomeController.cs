using System.Web;
using System.Web.Mvc;
using ContactManager.Models;
using ContactManager.Models.Validation;
using ContactManager.PPP.Intefaces;
using ContactManager.PPP.Services;
using ContactManager.Users.Interfaces;
using ContactManager.Users.Services;
using ContactManager.Users.ViewModels;
using ContactManager.Web.Helpers;

namespace ContactManager.Web.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        private readonly IUserFacade _facade;
        private readonly IProfileService _profileService;
        private readonly CurrentContext _ctx;

        public HomeController() 
        {
            var dictionary = new ModelStateWrapper(ModelState);
            _facade = new UserFacade(dictionary);
            _profileService = new ProfileService(dictionary);
            _ctx = new CurrentContext();
        }

        [Authorize]
        public ActionResult Index()
        {
            var client = _facade.GetContact(_ctx.CurrentUserId);
            client.LoadStatusReferences();
            client.LoadContractReferences();
            client.LoadClientServices();
            client.LoadAddressReferences();

            var viewModel = _facade.ClientService.GetViewModel(client);
            var profile = _profileService.GetProfile(client.ProfileId);
            viewModel.ProfileDisplayName = profile != null ? profile.DisplayName : "";
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


        public ActionResult Sitemap()
        {
            return View(SiteMap.RootNode);
        }
    }
}