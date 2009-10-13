using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using ContactManager.Intefaces;
using ContactManager.Models.Validation;
using ContactManager.Models;
using ContactManager.Helpers;

namespace ContactManager.Controllers
{
    public class QueueSimpleController : Controller
    {
        private IQueueSimpleService _service;

        public QueueSimpleController()
        {
            IValidationDictionary validationDictionary = new ModelStateWrapper(this.ModelState);
            _service = new QueueSimpleService(validationDictionary);
        }

        public QueueSimpleController(IQueueSimpleService service)
        {
            _service = service;
        }

        //
        // GET: /QueueSimple/

        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            return View(_service.ListQueues());
        }

        //
        // GET: /QueueSimple/Details/5

        [Authorize(Roles = "admin")]
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /QueueSimple/Create

        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            FillViewData(null);
            return View();
        }

        //
        // POST: /QueueSimple/Create

        [Authorize(Roles = "admin")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(QueueSimple queue)
        {
            if (_service.CreateQueue(queue))
                return RedirectToAction("Index");
            FillViewData(null);
            return View();
        }

        //
        // GET: /QueueSimple/Edit/5

        [Authorize(Roles = "admin")]
        public ActionResult Edit(int id)
        {
            var queue = _service.GetQueue(id);
            FillViewData(queue);
            return View(queue);
        }

        //
        // POST: /QueueSimple/Edit/5

        [Authorize(Roles = "admin")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(QueueSimple queue)
        {
            if(_service.EditQueue(queue))
                return RedirectToAction("Index");
            FillViewData(queue);
            return View(queue);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            if (_service.DeleteQueue(id))
                return RedirectToAction("Index");
            return View("Index", _service.ListQueues());
        }

        [Authorize(Roles = "admin")]
        public ActionResult DeleteAll()
        {
            foreach (var queue in _service.ListQueues())
            {
                try
                {
                    _service.DeleteQueue(queue.QueueId);
                }
                catch { }
            }
            return View("Index", _service.ListQueues());
        }

        private void FillViewData(QueueSimple queue)
        {
            var ddHelper = new DropDownHelper();
            if (queue == null)
                ViewData["Tariffs"] = ddHelper.GetTariffs(null);
            else
                ViewData["Tariffs"] = ddHelper.GetTariffs(queue.TariffId);
        }
    }
}
