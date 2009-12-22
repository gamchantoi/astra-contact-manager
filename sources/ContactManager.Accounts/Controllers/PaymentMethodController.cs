using System.Web.Mvc;
using ContactManager.Accounts.Interfaces;
using ContactManager.Accounts.Models;
using ContactManager.Models;
using ContactManager.Models.Validation;

namespace ContactManager.Accounts.Controllers
{
    public class PaymentMethodController : Controller
    {
        private readonly IValidationDictionary validationDictionary;
        private readonly IPaymentMethodService _paymentMethodService;

        public PaymentMethodController()
        {
            validationDictionary = new ModelStateWrapper(ModelState);
            _paymentMethodService = new PaymentMethodService(validationDictionary);
        }


        public ActionResult Index()
        {
            return View(_paymentMethodService.ListPaymentMethods(null));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(PaymentMethod paymentMethod)
        {
            if (_paymentMethodService.Create(paymentMethod))
                return RedirectToAction("Index");
            return View(paymentMethod);
        }
 
        public ActionResult Edit(int id)
        {
            return View(_paymentMethodService.GetPaymentMethod(id));
        }

        [HttpPost]
        public ActionResult Edit(PaymentMethod paymentMethod)
        {
            if (_paymentMethodService.EditPaymentMethod(paymentMethod))
                return RedirectToAction("Index");
            return View(paymentMethod);
        }
    }
}
