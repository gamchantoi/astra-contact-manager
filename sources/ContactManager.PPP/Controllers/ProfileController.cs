using System.Web.Mvc;
using ContactManager.Hosts.Helpers;
using ContactManager.Models;
using ContactManager.Models.Validation;
using System.Collections.Generic;
using ContactManager.PPP.Intefaces;
using ContactManager.PPP.Models;
using ContactManager.PPP.SSH;
using ContactManager.PPP.Helpers;

namespace ContactManager.PPP.Controllers
{
    [HandleError]
    public class ProfileController : Controller
    {
        private DropDownHelper _ddhelper = new DropDownHelper();
        private HostHelper _uhelper = new HostHelper();
        private IProfileService _service;
        private ISshProfileService _sshService;

        public ProfileController()
        {
            IValidationDictionary validationDictionary = new ModelStateWrapper(ModelState);
            _service = new ProfileService(validationDictionary);
            _sshService = new SshProfileService(validationDictionary);
        }

        //
        // GET: /Tariff/
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {            
            return View(PrepareIndex());
        }

        //
        // GET: /Tariff/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {            
            ViewData["Pools"] = _ddhelper.GetPools(null);
            return View();
        } 

        //
        // POST: /Tariff/Create
        [Authorize(Roles = "admin")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([Bind(Exclude = "ProfileId")]Profile profile)
        {
            if (_service.CreateProfile(profile))
            {
                _sshService.Connect(_uhelper.GetCurrentHost());
                var result = _sshService.CreatePPPProfile(profile.ProfileId);
                _sshService.Disconnect();
                return View("Index", PrepareIndex());
            }
            ViewData["Pools"] = _ddhelper.GetPools(null);
            return View();
        }

        //
        // GET: /Tariff/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int id)
        {
            ViewData["Pools"] = _ddhelper.GetPools(id);
            return View(_service.GetProfile(id));
        }

        //
        // POST: /Tariff/Edit/5
        [Authorize(Roles = "admin")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(Profile profile)
        {
            if (_service.EditProfile(profile))
            {
                _sshService.Connect(_uhelper.GetCurrentHost());
                var result = _sshService.EditPPPProfile(profile.ProfileId);
                _sshService.Disconnect();
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
                _sshService.Connect(_uhelper.GetCurrentHost());
                var result = _sshService.DeletePPPProfile(profile.Name);
                _sshService.Disconnect();
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