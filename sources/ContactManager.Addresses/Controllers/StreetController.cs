using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using ContactManager.Addresses.Interfaces;
using ContactManager.Addresses.Services;
using ContactManager.Models;
using ContactManager.Models.Validation;

namespace ContactManager.Addresses.Controllers
{
    public class StreetController : Controller
    {
        private readonly IValidationDictionary validationDictionary;
        private readonly IStreetService _streetService;

        public StreetController()
        {
            validationDictionary = new ModelStateWrapper(ModelState);
            _streetService = new StreetService(validationDictionary);
        }

        public ActionResult Index()
        {
            return View(_streetService.ListStreets());
        }


        public ActionResult Details(int id)
        {
            return View();
        }


        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(Street street)
        {
            if (_streetService.CreateStreet(street))
                return View("Index");
            return View(street);
        }


        public ActionResult Edit(int id)
        {
            return View(_streetService.GetStreet(id));
        }


        [HttpPost]
        public ActionResult Edit(Street street)
        {
            if (_streetService.EditStreet(street))
                return RedirectToAction("Index");
            return View(street);
            
        }
    }
}
