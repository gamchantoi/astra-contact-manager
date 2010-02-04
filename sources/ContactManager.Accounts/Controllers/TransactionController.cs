using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using ContactManager.Accounts.Helpers;
using ContactManager.Accounts.Interfaces;
using ContactManager.Accounts.Services;
using ContactManager.Accounts.ViewModels;
using ContactManager.Models;
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

            var list = isAdmin ? _service.ListTransactions() 
                : _service.ListTransactions(_userHelper.CurrentUserId);

            Mapper.CreateMap<Transaction, Transactions>();
            var viewModelList = new TransactionViewModel();
            viewModelList.Transactions = Mapper.Map<IList<Transaction>, IList<Transactions>>(list);
            viewModelList.Filter = _service.GetFilter();
            return View(viewModelList);
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult FiltredList(Filter filter)
        {

            var _userHelper = new UserHelper();
            var isAdmin = _userHelper.IsUserInRole("admin");
            ViewData["IsAdmin"] = isAdmin;

            var list = isAdmin ? _service.ListTransactions(filter)
                : _service.ListTransactions(_userHelper.CurrentUserId);

            Mapper.CreateMap<Transaction, Transactions>();
            var viewModelList = new TransactionViewModel();
            viewModelList.Transactions = Mapper.Map<IList<Transaction>, IList<Transactions>>(list);
            viewModelList.Filter = _service.GetFilter();
            return View("Index", viewModelList);
        }

        [Authorize(Roles = "admin")]
        public ActionResult ProcessClientPayment()
        {
            _service.ProcessClientPayment();
            return RedirectToAction("Index");
        }
    }
}