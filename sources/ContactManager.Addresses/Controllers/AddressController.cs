using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using ContactManager.Addresses.Services;
using ContactManager.Models;
using ContactManager.Models.Validation;

namespace ContactManager.Addresses.Controllers
{
    public class AddressController : Controller
    {
        private readonly AddressService _addressService;
        private CurrentContext _ctx;

        public AddressController()
        {
            _ctx = new CurrentContext();
            _addressService = new AddressService(new ModelStateWrapper(ModelState));
        }

        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            return View(_addressService.ListAddresses());
        }

        [Authorize]
        public ActionResult Create(Guid id)
        {
            ViewData["UserId"] = id;
            FillViewData(null);
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([Bind(Exclude = "AddressId")]Address address)
        {
            if (_addressService.CreateAddress(address))
                return RedirectToAction("Edit", "User", new { id = address.UserId, area = "Users" });
            FillViewData(address);
            return View(address);
        }

        public ActionResult Edit(Guid id)
        {
            var address = _addressService.GetAddress(id);
            FillViewData(address);
            return View(address);
        }

        public ActionResult EditForAddress(int id)
        {
            var address = _addressService.GetAddress(id);
            FillViewData(address);
            return View(address);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditForAddress(Address address)
        {
            if (_addressService.EditAddress(address))
                return RedirectToAction("Index");
            FillViewData(address);
            return View(address);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(Address address)
        {
            if (_addressService.EditAddress(address))
                return RedirectToAction("Edit", "User", new { id = address.UserId, area = "Users" });
            FillViewData(address);
            return View(address);
        }

        private void FillViewData(Address address)
        {
            if (address != null)
                ViewData["Streets"] = _addressService.ListStreets(address.Street.StreetId);
            else
                ViewData["Streets"] = _addressService.ListStreets(null);
        }
    }
}
