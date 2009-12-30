using System.Web.Mvc;
using ContactManager.Models;
using ContactManager.Models.Validation;
using System.Collections.Generic;
using ContactManager.PPP.Intefaces;
using ContactManager.PPP.Services;
using ContactManager.PPP.SSH;
using ContactManager.PPP.Helpers;

namespace ContactManager.PPP.Controllers
{
    [HandleError]
    public class ProfileController : Controller
    {
        private readonly DropDownHelper _ddhelper;
        private readonly IProfileService _service;
        private readonly ISshProfileService _sshService;

        public ProfileController()
        {
            _ddhelper = new DropDownHelper();
            IValidationDictionary validationDictionary = new ModelStateWrapper(ModelState);
            _service = new ProfileService(validationDictionary);
            _sshService = new SshProfileService(validationDictionary);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {            
            return View(PrepareIndex());
        }

        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {            
            ViewData["Pools"] = _ddhelper.GetPools(null);
            return View();
        } 

        [Authorize(Roles = "admin")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([Bind(Exclude = "ProfileId")]Profile profile)
        {
            if (_service.CreateProfile(profile))
            {
                _sshService.CreatePPPProfile(profile.ProfileId);
                return View("Index", PrepareIndex());
            }
            ViewData["Pools"] = _ddhelper.GetPools(null);
            return View();
        }


        [Authorize(Roles = "admin")]
        public ActionResult Edit(int id)
        {
            ViewData["Pools"] = _ddhelper.GetPools(id);
            return View(_service.GetProfile(id));
        }


        [Authorize(Roles = "admin")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(Profile profile)
        {
            if (_service.EditProfile(profile))
            {
                //var _profile = _service.GetProfile(profile.ProfileId);
                _sshService.EditPPPProfile(profile.ProfileId);
                return View("Index", PrepareIndex());
            }
            return View(profile);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            var profile = _service.GetProfile(id);
            if (_service.DeleteProfile(id))
            {
                _sshService.DeletePPPProfile(profile.Name);
            }

            return View("Index", PrepareIndex());
        }

        [Authorize(Roles = "admin")]
        public ActionResult DeleteUnused()
        {
            foreach (var profile in _service.ListProfiles())
                Delete(profile.ProfileId);
            return View("Index", PrepareIndex());
        }

        private List<Profile> PrepareIndex()
        {
            var profiles = _service.ListProfiles();
            profiles.Sort((c1, c2) => c1.Name.CompareTo(c2.Name));
            return profiles;
        }
    }
}