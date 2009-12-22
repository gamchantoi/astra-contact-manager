using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using ContactManager.Models;
using ContactManager.Models.Validation;
using ContactManager.Models.ViewModels;
using ContactManager.PPP.Intefaces;
using ContactManager.PPP.SSH;
using ContactManager.Users.Interfaces;
using ContactManager.Users.Services;
using ContactManager.Users.Helpers;
using ContactManager.Users.ViewModels;

namespace ContactManager.Users.Controllers
{
    [HandleError]
    public class UserController : Controller
    {
        private readonly IValidationDictionary validationDictionary;
        private readonly IUserFasade _facade;
        private readonly ISshSecretService _sshSecretService;
        private readonly CurrentContext _ctx;
        private readonly IStatusService _statusService;

        public UserController()
        {
            _ctx = new CurrentContext();
            validationDictionary = new ModelStateWrapper(ModelState);
            _facade = new UserFasade(validationDictionary);
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
            if (_facade.CreateContact(client))
            {
                if (_facade.CanSynchronize(client.UserId))
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
            var client = _facade.GetContact(id);
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
            if (_facade.EditContact(client))
            {
                //todo: check client status for sync
                if (_facade.CanSynchronize(client.UserId))
                {
                    _sshSecretService.Connect(_ctx.GetCurrentHost());
                    var result = _sshSecretService.EditPPPSecret(client.UserId);
                    _sshSecretService.Disconnect();
                }
                return RedirectToAction("Index", PrepareIndex(false));
            }
            var wClient = _facade.GetContact(client.UserId);
            FillViewData(wClient);
            return View(wClient);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Status(Guid id, bool status)
        {
            var name = _facade.GetName(id);
            var canSync = _facade.CanSynchronize(id);
            if (status)
            {
                if (_facade.ActivateContact(id))
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
                if (_facade.DeleteContact(id))
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
            foreach (var user in _facade.ListContacts("client"))
            {
                _facade.DeleteContact(user.UserId);
            }
            return View("Index", PrepareIndex(false));
        }

        private ListClientViewModel PrepareIndex(bool deleted)
        {
            var _model = new ListClientViewModel();
            var users = _facade.ListContacts(deleted);
            users.Sort((c1, c2) => c1.UserName.CompareTo(c2.UserName));

            Mapper.CreateMap<Client, ClientViewModel>();
            var viewModelList = Mapper.Map<List<Client>, List<ClientViewModel>>(users);

            _model.Clients = viewModelList;
            _model.TotalUsers = users.Count();
            _model.TotalBalance = users.Sum(u => u.Balance);
            _model.Deleted = deleted;
            return _model;
        }

        [Authorize(Roles = "admin")]
        public ActionResult ClearAllData()
        {
            _facade.DeleteAllData();
            return View("Index", PrepareIndex(false));
        }

        [Authorize(Roles = "admin")]
        public ActionResult Load(Guid id)
        {
            return View(_facade.LoadMoneyService.GetViewModel(id));
        }

        [Authorize(Roles = "admin")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Load(LoadMoneyViewModel model)
        {
            if (_facade.LoadMoneyService.LoadMoney(model))
                return View("Index", PrepareIndex(false));
            var loadModel = _facade.LoadMoneyService.GetViewModel(model.ClientId);
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