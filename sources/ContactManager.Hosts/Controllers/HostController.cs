using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using ContactManager.Hosts.Models;
using ContactManager.Models;
using ContactManager.Hosts.Interfaces;
using ContactManager.Hosts;
using ContactManager.Models.Validation;

namespace ContactManager.Hosts.Controllers
{
    [HandleError]
    public class HostController : Controller
    {

        private IHostsService _service;

        public HostController()
        {
            IValidationDictionary validationDictionary = new ModelStateWrapper(ModelState);
            _service = new HostsService(validationDictionary);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {

            return View(_service.ListHosts());
        }


        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }


        [Authorize(Roles = "admin")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([Bind(Exclude = "HostId")] Host host)
        {

            if (_service.CreateHost(host))
                return RedirectToAction("Index");
            return View();

        }


        [Authorize(Roles = "admin")]
        public ActionResult Edit(int id)
        {
            return View(_service.GetHost(id));
        }


        [Authorize(Roles = "admin")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(Host host)
        {
            if (_service.EditHost(host))
                return RedirectToAction("Index");
            return View(host);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Select(int id) 
        {
            HttpContext.Profile.SetPropertyValue("HostId", id);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            _service.DeleteHost(id);
            return RedirectToAction("Index");
        }
    }
}