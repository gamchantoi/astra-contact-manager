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
            return deleted.HasValue ? View(PrepareIndex(deleted.Value)) : View(PrepareIndex(false));
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
            client.LoadStatusReferences();
            client.LoadContractReferences();
            client.LoadClientServices();
            client.LoadAddressReferences();
            return View(FillViewData(client));
        }

        [Authorize(Roles = "admin")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(ClientViewModel viewModel)
        {
            if (_facade.EditContact(viewModel))
            {
                //todo: check client status for sync
                if (_facade.CanSynchronize(viewModel.UserId))
                {
                    _sshSecretService.EditPPPSecret(viewModel.UserId);
                }
                return RedirectToAction("Index", PrepareIndex(false));
            }
            var wClient = _facade.GetContact(viewModel.UserId);
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

        private ClientViewModel FillViewData(Client client)
        {
            var viewModel = _facade.ClientService.GetViewModel(client);

            var userHelper = new DropDownHelper();
            var pppHelper = new PPP.Helpers.DropDownHelper();

            if (client == null)
            {
                viewModel.Roles = userHelper.GetRoles("client");
                viewModel.Profiles = pppHelper.GetProfiles(null);
                viewModel.Statuses = _statusService.ListStatuses(null);
            }
            else
            {
                viewModel.Roles = userHelper.GetRoles(client.Role);
                viewModel.Profiles = pppHelper.GetProfiles(client.ProfileId);
                viewModel.Statuses = _statusService.ListStatuses(client.Status.StatusId);
            }
            return viewModel;
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