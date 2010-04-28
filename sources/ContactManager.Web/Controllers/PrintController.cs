using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using ContactManager.Models;
using ContactManager.Models.Validation;
using ContactManager.Web.Interfaces;
using ContactManager.Web.Models;

namespace ContactManager.Web.Controllers
{
    public class PrintController : Controller
    {
        private readonly ICustomResourcesService _customResorces;
        private readonly IValidationDictionary validationDictionary;
        
        public PrintController()
        {
            validationDictionary = new ModelStateWrapper(ModelState);
            _customResorces = new CustomResourcesService(validationDictionary);
        }


        [Authorize(Roles = "admin")]
        public ActionResult EditContract()
        {
            var customResource = _customResorces.GetResource("Contract_View");
            if (customResource == null)
            {
                var newCustomResource = new CustomResource { Key = "Contract_View" };
                return View(newCustomResource);
            }
            return View(customResource);
        }


        //[Authorize(Roles = "admin")]
        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult EditContract(CustomResource resource)
        //{
        //    return View(_customResorces.EditResource(resource));
        //}


        [Authorize(Roles = "admin")]
        public ActionResult EditOrder()
        {
            var customResource = _customResorces.GetResource("Order_View");
            if (customResource == null)
            {
                var newCustomResource = new CustomResource { Key = "Order_View" };
                return View(newCustomResource);
            }
            return View(customResource);
        }

        //[Authorize(Roles = "admin")]
        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult EditOrder(CustomResource resource)
        //{
        //    return View(_customResorces.EditResource(resource));
        //}

        [Authorize(Roles = "admin")]
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult EditOrder(string customText, string Key)
        {
            var resource = new CustomResource{Key = Key, Value = customText};
            return View(_customResorces.EditResource(resource));
        }

    }
}
