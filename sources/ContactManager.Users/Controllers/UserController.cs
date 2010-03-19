using System;
using System.Linq;
using System.Web.Mvc;
using ContactManager.Models;
using ContactManager.Models.Enums;
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
        private readonly IUserFacade _facade;
        private readonly ISshSecretService _sshSecretService;
        private readonly CurrentContext _ctx;
        private readonly IStatusService _statusService;

        public UserController()
        {
            _ctx = new CurrentContext();
            validationDictionary = new ModelStateWrapper(ModelState);
            _facade = new UserFacade(validationDictionary);
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

        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View(FillViewData(null));
        }

        [Authorize(Roles = "admin")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(ClientViewModel viewModel)
        {
            if (_facade.CreateContact(viewModel))
            {
                if (_facade.CanSynchronize(viewModel.UserId))
                {
                    _sshSecretService.CreatePPPSecret(viewModel.UserId);
                }
                return View("Index", PrepareIndex(false));
            }
            return View(FillViewData(_facade.ClientService.GetModel(viewModel)));
        }

        [Authorize(Roles = "admin")]
        public ActionResult Edit(Guid id)
        {
            var client = _facade.GetContact(id);
            client.LoadStatusReferences();
            client.LoadContractReferences();
            client.LoadClientServices();
            client.LoadAddressReferences();
            //TempData["Message"] = id;
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
                return View("Index", PrepareIndex(false));
            }
            //var wClient = _facade.GetContact(viewModel.UserId);
            return View(FillViewData(_facade.ClientService.GetModel(viewModel)));
        }

        [Authorize(Roles = "admin")]
        public ActionResult Status(Guid id, bool status)
        {
            var name = _facade.GetContact(id).UserName;
            var canSync = _facade.CanSynchronize(id);
            if (status)
            {
                if (_facade.ActivateContact(id))
                {
                    if (canSync)
                    {
                        _sshSecretService.CreatePPPSecret(id);
                    }
                }
            }
            else
            {
                if (_facade.DeleteContact(id))
                {
                    if (canSync)
                    {
                        _sshSecretService.DeletePPPSecret(name);
                    }
                }
            }

            return View("Index", PrepareIndex(status));
        }

        private ClientViewModel FillViewData(Client client)
        {
            ClientViewModel viewModel;

            var userHelper = new DropDownHelper();
            var pppHelper = new PPP.Helpers.DropDownHelper();

            if (client == null)
            {
                viewModel = new ClientViewModel
                                {
                                    Roles = userHelper.GetRoles(ROLES.client.ToString()),
                                    Profiles = pppHelper.GetProfiles(null),
                                    Statuses = _statusService.ListStatuses(null)
                                };
            }
            else
            {
                viewModel = _facade.ClientService.GetViewModel(client);
                viewModel.Roles = userHelper.GetRoles(client.Role);
                viewModel.Profiles = pppHelper.GetProfiles(client.ProfileId);
                //viewModel.Statuses = _statusService.ListStatuses(client.Status.StatusId);// 2---------------
                viewModel.Statuses = _statusService.ListStatuses(client.Status.StatusId);
                viewModel.StatusId = client.Status.StatusId;
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
            _model.Clients = _facade.UserService.ListUsersModels();
            _model.Clients.Sort((c1, c2) => c1.UserName.CompareTo(c2.UserName));
            _model.TotalUsers = _model.Clients.Count();
            _model.TotalBalance = _model.Clients.Sum(u => u.Balance);
            _model.Deleted = deleted;
            return _model;
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
            {
                if (model.NeedUpdate)
                    _sshSecretService.EditPPPSecret(model.ClientId);
                return View("Index", PrepareIndex(false));
            }
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