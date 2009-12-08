using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using ContactManager.Accounts.Interfaces;
using ContactManager.Accounts.Models;
using ContactManager.Models;

namespace ContactManager.Accounts.Controllers
{
    public class PaymentMethodController : Controller
    {

        private IPaymentMethodService _paymentMethodService = new PaymentMethodService();


        public ActionResult Index()
        {
            return View(_paymentMethodService.ListPaymentMethods());
        }



        //public ActionResult Details(int id)
        //{
        //    return View (_paymentMethodService.GetPaymentMethod(id));
        //}



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
