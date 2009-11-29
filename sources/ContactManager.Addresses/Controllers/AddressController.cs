using System.Web.Mvc;
using ContactManager.Addresses.Services;
using ContactManager.Models;
using ContactManager.Models.Validation;

namespace ContactManager.Addresses.Controllers
{
    public class AddressController : Controller
    {
        private readonly AddressService _addressService;

        public AddressController()
        {
            _addressService = new AddressService(new ModelStateWrapper(ModelState));
        }

        public ActionResult Index()
        {
            return View(_addressService.ListAddresses());
        }

        public ActionResult Create()
        {
            FillViewData(null);
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(Address address)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Address/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Address/Edit/5

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private void FillViewData(Address address)
        {
            if (address != null)
            {
                ViewData["Streets"] = _addressService.ListStreets(address.Street.StreetId);
            }
            ViewData["Streets"] = _addressService.ListStreets(null);
        }
    }
}
