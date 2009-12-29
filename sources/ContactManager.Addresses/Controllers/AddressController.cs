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
        public ActionResult Create([Bind(Exclude = "AddressId")]Address address)
        {
            if (_addressService.CreateAddress(address))
                return RedirectToAction("Index");
            FillViewData(address);
            return View(address);
        }

        public ActionResult Edit(int id)
        {
            Address _address = _addressService.GetAddress(id);
            FillViewData(_address);
            return View(_address);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(Address address)
        {
            
            if (_addressService.EditAddress(address))
                return RedirectToAction("Index");
            FillViewData(address);
            return View(address);
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
