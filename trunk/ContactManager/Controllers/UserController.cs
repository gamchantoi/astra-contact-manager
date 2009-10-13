using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ContactManager.Controllers;
using ContactManager.Helpers;
using ContactManager.Intefaces;
using ContactManager.Models;
using ContactManager.Models.Validation;

namespace ContactManager.Controllers
{
    [HandleError]
    public class UserController : BaseController
    {
        private readonly IContactService _service;
        private readonly ISSHService _sshService;
        private readonly IServiceService _serviceService;
        private readonly IClientServiceActivitiyService _serviceActivity;
        private readonly IPPPSecretService _pppSecretService;
        private readonly UserHelper _helper = new UserHelper();

        public UserController()
        {
            IValidationDictionary validationDictionary = new ModelStateWrapper(ModelState);
            _service = new ContactService(validationDictionary);
            _sshService = new SSHService(validationDictionary);
            _serviceService = new ServiceService(validationDictionary);
            _serviceActivity = new ClientServiceActivitiyService(validationDictionary);
            _pppSecretService = new PPPSecretService(validationDictionary);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Index(bool? deleted)
        {
            if (deleted.HasValue)
                return View(PrepareIndex(deleted.Value));
            return View(PrepareIndex(false));
        }

        //public ActionResult IndexAJAX()
        //{
        //    var callback = Request["callback"].ToString();
        //    Response.Write(callback + "(" + ExtJsJsonSerializer.JsonSerializeList<List<Client>>(_service.ListContacts()) + ")");
        //    return null;
        //}

        //
        // GET: /User/Details/5
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
                    _sshService.Connect(_helper.GetCurrentHost());
                    var result = _sshService.CreatePPPSecret(client.UserId);
                    _sshService.Disconnect();
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
                if (_service.CanSynchronize(client.UserId) && client.Status == 1)
                {
                    _sshService.Connect(_helper.GetCurrentHost());
                    var result = _sshService.EditPPPSecret(client.UserId);
                    _sshService.Disconnect();
                }
                return RedirectToAction("Index", PrepareIndex(client.Status == 0));
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
                        _sshService.Connect(_helper.GetCurrentHost());
                        _sshService.CreatePPPSecret(id);
                        _sshService.Disconnect();
                    }
                }
            }
            else
            {
                if (_service.DeleteContact(id))
                {
                    if (canSync)
                    {
                        _sshService.Connect(_helper.GetCurrentHost());
                        _sshService.DeletePPPSecret(name);
                        _sshService.Disconnect();
                    }
                }
            }

            return View("Index", PrepareIndex(status));
        }

        private void FillViewData(Client user)
        {
            var ddHelper = new DropDownHelper();

            if (user == null)
            {
                ViewData["Roles"] = ddHelper.GetRoles("client");
                ViewData["Profiles"] = ddHelper.GetProfiles(null);
                ViewData["Statuses"] = ddHelper.GetStatuses("Active");
                ViewData["Methods"] = ddHelper.GetAccountActivitiesMethods(null);
            }
            else
            {
                ViewData["Roles"] = ddHelper.GetRoles(user.Role);
                ViewData["Profiles"] = ddHelper.GetProfiles(user.ProfileId);
                ViewData["Statuses"] = ddHelper.GetStatuses(user.SecretStatus);
                ViewData["Methods"] = ddHelper.GetAccountActivitiesMethods(null);
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
            var ddHelper = new DropDownHelper();
            var client = _service.GetContact(id);

            client.astra_ClientsDetailsReference.Load();
            ViewData["UserId"] = id;
            ViewData["UserName"] = client.UserName;
            ViewData["Balance"] = client.Balance;
            ViewData["Methods"] = ddHelper.GetAccountActivitiesMethods(null);

            var details = client.astra_ClientsDetailsReference.Value;
            if (details != null)
                ViewData["UserDetail"] = string.Format("{0} {1} {2}", details.LastName, details.FirstName, details.MiddleName);
            return View(client.astra_ClientsDetailsReference.Value);
        }

        [Authorize(Roles = "admin")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Load(Guid UserId, int MethodId, Decimal Load, string Comment)
        {
            var client = _service.GetContact(UserId);
            client.MethodId = MethodId;
            client.Load = Load;
            client.Comment = Comment;
            _service.LoadMoney(client);
            return View("Index", PrepareIndex(client.Status == 0));
        }

        [Authorize(Roles = "admin")]
        public ActionResult Services(Guid id)
        {
            var client = _service.GetContact(id);
            var services = _serviceService.ListServices(Helpers.Status.Active);
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
            if (_pppSecretService.UpdatePPPSecretAddresses(secret) &&
                _serviceService.UpdateSystemService(secret))
            {
                _sshService.Connect(_helper.GetCurrentHost());
                _sshService.EditPPPSecret(secret.UserId);
                _sshService.Disconnect();

                return RedirectToAction("Index", PrepareIndex(false));
            }
            return RedirectToAction("Edit", secret.UserId);
        }
    }
}