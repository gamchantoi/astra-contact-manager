using System;
using System.Linq;
using System.Web.Mvc;
using ContactManager.Models.Validation;
using ContactManager.Models;
using ContactManager.Services.Interfaces;
using ContactManager.Services.Models;

namespace ContactManager.Services.Controllers
{
    [HandleError]
    public class ServiceController : Controller
    {
        private readonly IServiceService _service;
        private readonly IClientInServicesService _clientInServicesService;

        public ServiceController()
        {
            IValidationDictionary validationDictionary = new ModelStateWrapper(ModelState);
            _service = new ServiceService(validationDictionary);
            _clientInServicesService = new ClientInServicesService(validationDictionary);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            return View(_service.ListServices(null).ToList());
        }

        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(Service service)
        {
            if (_service.CreateService(service))
                return RedirectToAction("Index");
            return View();
        }

        [Authorize(Roles = "admin")]
        public ActionResult Edit(int id)
        {
            return View(_service.GetService(id));
        }

        [Authorize(Roles = "admin")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(Service service)
        {
            if (_service.EditService(service))
                return RedirectToAction("Index");
            return View(service);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Activities()
        {
            return View(_service.ListActivities());
        }


        [Authorize(Roles = "admin")]
        public ActionResult ClientServices(Guid id)
        {
            return View(_clientInServicesService.GetClientServices(id));
        }

        [Authorize(Roles = "admin")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ClientServices(FormCollection collection, Guid id)
        {
            if (_clientInServicesService.UpdateClientServices(collection, id))
                return RedirectToAction("Index", "User", new {Area="Users"});
            return View(collection);
        }
    }
}