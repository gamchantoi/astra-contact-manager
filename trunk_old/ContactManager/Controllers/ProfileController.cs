using System;
using System.Linq;
using System.Web.Mvc;
using ContactManager.Models;
using ContactManager.Controllers;
using ContactManager.Intefaces;
using ContactManager.Models.Validation;
using ContactManager.Helpers;
using System.Collections.Generic;

namespace ContactManager.Controllers
{
    [HandleError]
    public class ProfileController : BaseController
    {
        private DropDownHelper _ddhelper = new DropDownHelper();
        private UserHelper _uhelper = new UserHelper();
        private IProfileService _service;
        private ISSHService _sshService;

        public ProfileController()
        {
            IValidationDictionary validationDictionary = new ModelStateWrapper(this.ModelState);
            _service = new ProfileService(validationDictionary);
            _sshService = new SSHService(validationDictionary);
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