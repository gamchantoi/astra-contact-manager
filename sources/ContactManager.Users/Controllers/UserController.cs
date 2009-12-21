using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
//using ContactManager.Clients;
using ContactManager.Models;
using ContactManager.Models.Validation;
using ContactManager.Models.ViewModels;
using ContactManager.PPP.Intefaces;
using ContactManager.PPP.SSH;
using ContactManager.Users.Interfaces;
using ContactManager.Users.Services;
using ContactManager.Users.Helpers;

namespace ContactManager.Users.Controllers
{
    [HandleError]
    public class UserController : Controller
    {
        private readonly IValidationDictionary validationDictionary;
        private readonly IUserFasade _service;
        private readonly ISshSecretService _sshSecretService;
        private readonly CurrentContext _ctx;
        private readonly IStatusService _statusService;

        public UserController()
        {
            _ctx = new CurrentContext();
            validationDictionary = new ModelStateWrapper(ModelState);
            _service = new UserFasade(validationDictionary);
            _sshSecretService = new SshSecretService(validationDictionary, true);
            _statusService = new StatusService(validationDictionary);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Index(bool? deleted)
        {
            if (deleted.HasValue)
                return View(PrepareIndex(deleted.Value));
            return View(PrepareIndex(false));
        }

        [Authorize(Roles = "admin")]
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            //ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
            FillViewData(null);
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(Client client)
        {
            if (_service.CreateContact(client))
            {
                if (_service.CanSynchronize(client.UserId))
                {
                    _sshSecretService.CreatePPPSecret(client.UserId);
                }
                return View("Index", PrepareIndex(false));
            }
            FillViewData(null);
            return View();
        }

        [Authorize(Roles = "admin")]
        public ActionResult Edit(Guid id)
        {
            var client = _service.GetContact(id);
            client.LoadClientServices();
            FillViewData(client);
            return View(client);
        }

        [Authorize(Roles = "admin")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(Client client, string button)
        {
            if (button.Equals("Details"))
            {
                return RedirectToAction("Index", "Detail", new { id = client.UserId });
            }
            if (_service.EditContact(client))
            {
                //todo: check client status for sync
                if (_service.CanSynchronize(client.UserId))
                {
                    _sshSecretService.Connect(_ctx.GetCurrentHost());
                    var result = _sshSecretService.EditPPPSecret(client.UserId);
                    _sshSecretService.Disconnect();
                }
                return RedirectToAction("Index", PrepareIndex(false));
            }
            var wClient = _service.GetContact(client.UserId);
            FillViewData(wClient);
            return View(wClient);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Status(Guid id, bool status)
        {
            var name = _service.GetName(id);
            var canSync = _service.CanSynchronize(id);
            if (status)
            {
                if (_service.ActivateContact(id))
                {
                    if (canSync)
                    {
                        _sshSecretService.Connect(_ctx.GetCurrentHost());
                        _sshSecretService.CreatePPPSecret(id);
                        _sshSecretService.Disconnect();
                    }
                }
            }
            else
            {
                if (_service.DeleteContact(id))
                {
                    if (canSync)
                    {
                        _sshSecretService.Connect(_ctx.GetCurrentHost());
                        _sshSecretService.DeletePPPSecret(name);
                        _sshSecretService.Disconnect();
                    }
                }
            }

            return View("Index", PrepareIndex(status));
        }

        private void FillViewData(Client user)
        {                        // ���� � ����� ���� ���� � ���
            var userHelper = new DropDownHelper();
            var pppHelper = new PPP.Helpers.DropDownHelper();

            if (user == null)
            {
                ViewData["Roles"] = userHelper.GetRoles("client");
                ViewData["Profiles"] = pppHelper.GetProfiles(null);
                ViewData["Statuses"] = _statusService.ListStatuses(null);
            }
            else
            {
                ViewData["Roles"] = userHelper.GetRoles(user.Role);
                ViewData["Profiles"] = pppHelper.GetProfiles(user.ProfileId);
                ViewData["Statuses"] = _statusService.ListStatuses(user.StatusId);
            }
        }

        [Authorize(Roles = "admin")]
        public ActionResult DeleteAll()
        {
            foreach (var user in _service.ListContacts("client"))
            {
                _service.DeleteContact(user.UserId);
            }
            return View("Index", PrepareIndex(false));
        }

        private List<Client> PrepareIndex(bool deleted)
        {
            var users = _service.ListContacts(deleted);
            users.Sort((c1, c2) => c1.UserName.CompareTo(c2.UserName));
            ViewData["TotalUsers"] = users.Count();
            ViewData["TotalBalance"] = String.Format("{0:F}", users.Sum(u => u.Balance));
            ViewData["Deleted"] = deleted;
            return users;
        }

        [Authorize(Roles = "admin")]
        public ActionResult ClearAllData()
        {
            _service.DeleteAllData();
            return View("Index", PrepareIndex(false));
        }

        [Authorize(Roles = "admin")]
        public ActionResult Load(Guid id)
        {
            return View(_service.LoadMoneyService.GetViewModel(id));
        }

        [Authorize(Roles = "admin")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Load(LoadMoneyViewModel model)
        {
            if (_service.LoadMoneyService.LoadMoney(model))
                return View("Index", PrepareIndex(false));
            var loadModel = _service.LoadMoneyService.GetViewModel(model.ClientId);
            return View(loadModel);
        }



        //[Authorize(Roles = "admin")]
        //public ActionResult UpdateSecret(PPPSecret secret)
        //{
        //    var _pppSecretService = new SecretService(validationDictionary);
        //    if (_pppSecretService.UpdatePPPSecretAddresses(secret) &&
        //        _serviceService.UpdateSystemService(secret))
        //    {
        //        _sshSecretService.Connect(_helper.GetCurrentHost());
        //        _sshSecretService.EditPPPSecret(secret.UserId);
        //        _sshSecretService.Disconnect();

        //        return RedirectToAction("Index", PrepareIndex(false));
        //    }
        //    return RedirectToAction("Edit", secret.UserId);
        //}
    }
}