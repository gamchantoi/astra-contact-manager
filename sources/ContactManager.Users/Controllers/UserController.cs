using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ContactManager.Clients;
using ContactManager.Hosts.Helpers;
using ContactManager.Models;
using ContactManager.Models.Validation;
using ContactManager.PPP.Intefaces;
using ContactManager.PPP.Models;
using ContactManager.PPP.SSH;
using ContactManager.Services.Interfaces;
using ContactManager.Services.Models;
using ContactManager.Users.Interfaces;
using ContactManager.Users.Models;

namespace ContactManager.Users.Controllers
{
    [HandleError]
    public class UserController : Controller
    {
        private readonly IValidationDictionary validationDictionary;
        private readonly IContactService _service;
        private readonly IServiceService _serviceService;
        private readonly IClientInServicesService _serviceActivity;
        private readonly ISshSecretService _sshSecretService;
        private readonly HostHelper _helper = new HostHelper();
        private readonly IStatusService _statusService;

        public UserController()
        {
            validationDictionary = new ModelStateWrapper(ModelState);
            _service = new ContactService(validationDictionary);
            _serviceService = new ServiceService(validationDictionary);
            _serviceActivity = new ClientInServicesService(validationDictionary);
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
            client.LoadServicesActivities();
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
                    _sshSecretService.Connect(_helper.GetCurrentHost());
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
                        _sshSecretService.Connect(_helper.GetCurrentHost());
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
                        _sshSecretService.Connect(_helper.GetCurrentHost());
                        _sshSecretService.DeletePPPSecret(name);
                        _sshSecretService.Disconnect();
                    }
                }
            }

            return View("Index", PrepareIndex(status));
        }

        private void FillViewData(Client user)
        {
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
            List<Client> users = _service.ListContacts(deleted);
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
        public ActionResult Load(Guid UserId, int MethodId, Decimal Load, string Comment)
        {
            var client = _service.GetContact(UserId);
            client.MethodId = MethodId;
            client.Load = Load;
            client.Comment = Comment;
            //_service.LoadMoney(client);
            return View("Index", PrepareIndex(false));
        }

        [Authorize(Roles = "admin")]
        public ActionResult Services(Guid id)
        {
            var client = _service.GetContact(id);
            var services = _serviceService.ListServices(Statuses.Active);
            ViewData["UserId"] = id;
            ViewData["ClientServices"] = client.LoadServicesActivities();
            return View(services);
        }

        [Authorize(Roles = "admin")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Services(FormCollection collection, Guid UserId)
        {
            _serviceActivity.UpdateActivity(collection, UserId);
            return View("Index", PrepareIndex(false));
        }

        [Authorize(Roles = "admin")]
        public ActionResult UpdateSecret(PPPSecret secret)
        {
            var _pppSecretService = new SecretService(validationDictionary);
            if (_pppSecretService.UpdatePPPSecretAddresses(secret) &&
                _serviceService.UpdateSystemService(secret))
            {
                _sshSecretService.Connect(_helper.GetCurrentHost());
                _sshSecretService.EditPPPSecret(secret.UserId);
                _sshSecretService.Disconnect();

                return RedirectToAction("Index", PrepareIndex(false));
            }
            return RedirectToAction("Edit", secret.UserId);
        }
    }
}