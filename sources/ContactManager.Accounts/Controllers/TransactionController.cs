using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using ContactManager.Accounts.Helpers;
using ContactManager.Accounts.Interfaces;
using ContactManager.Accounts.Services;
using ContactManager.Accounts.ViewModels;
using ContactManager.Models;
using ContactManager.Models.Enums;
using ContactManager.Models.Validation;

namespace ContactManager.Accounts.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionService _service;

        public TransactionController()
        {
            _service = new TransactionService(new ModelStateWrapper(ModelState));
        }

        [Authorize]
        public ActionResult Index()
        {
            var _userHelper = new UserHelper();
            var isAdmin = _userHelper.IsUserInRole("admin");
            ViewData["IsAdmin"] = isAdmin;

            var list = isAdmin  ? _service.ListTransactions()
                                : _service.ListTransactions(_userHelper.CurrentUserId);

            Mapper.CreateMap<Transaction, Transactions>();
            var viewModelList = new TransactionViewModel();
            viewModelList.Transactions = Mapper.Map<IList<Transaction>, IList<Transactions>>(list);
            viewModelList.Filter = _service.GetFilter();
            viewModelList.TotalSum = viewModelList.Transactions.Sum(t => t.Sum);


            return View(viewModelList);
        }

        [Authorize]
        public ActionResult ClientTransactions(Guid clientUserId)
        {
            var list = _service.ListTransactions(clientUserId);

            Mapper.CreateMap<Transaction, Transactions>();
            var viewModelList = new TransactionViewModel();
            viewModelList.Transactions = Mapper.Map<IList<Transaction>, IList<Transactions>>(list);
            viewModelList.Filter = _service.GetFilter();
            viewModelList.TotalSum = viewModelList.Transactions.Sum(t => t.Sum);

            //return View("Index", viewModelList);
            return View(viewModelList);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult FiltredList(FormCollection formCollection)
        {
            var filter = BuildFilter(formCollection);
            
            var _userHelper = new UserHelper();
            var isAdmin = _userHelper.IsUserInRole(ROLES.admin.ToString());
            ViewData["IsAdmin"] = isAdmin;

            //------------------
            //var list = isAdmin  ? _service.ListTransactions(filter)
                                //: _service.ListTransactions(_userHelper.CurrentUserId);

            //Mapper.CreateMap<Transaction, Transactions>();
            var viewModelList = new TransactionViewModel();
            //viewModelList.Transactions = Mapper.Map<IList<Transaction>, IList<Transactions>>(list);
            viewModelList.Transactions = _service.ListTransactions(filter);
            viewModelList.Filter = _service.GetFilter();
            
            return View("Index", viewModelList);
        }

        private static Filter BuildFilter(FormCollection formCollection)
        {
            return new Filter
                             {
                                 Years = int.Parse(formCollection.GetValue("Years").AttemptedValue),
                                 Months = int.Parse(formCollection.GetValue("Months").AttemptedValue),
                                 PaymentMethods = formCollection.GetValue("PaymentMethods").AttemptedValue
                             };
        }

        [Authorize(Roles = "admin")]
        public ActionResult ProcessPayment()
        {
            _service.ProcessClientPayment();
            return RedirectToAction("Index");
        }
    }
}