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
            IValidationDictionary validationDictionary = new ModelStateWrapper(ModelState);
            _service = new TransactionService(validationDictionary);
        }

        [Authorize]
        public ActionResult Index()
        {
            var _userHelper = new UserHelper();
            var isAdmin = _userHelper.IsUserInRole("admin");
            ViewData["IsAdmin"] = isAdmin;

            var list = isAdmin ? _service.ListTransactions() 
                : _service.ListTransactions(_userHelper.CurrentUserId);

            Mapper.CreateMap<Transaction, TransactionVewModel>();
            var viewModelList = Mapper.Map<IList<Transaction>, IList<TransactionVewModel>>(list);
            
            return View(viewModelList);
        }

        [Authorize(Roles = "admin")]
        public ActionResult ProcessClientPayment()
        {
            _service.ProcessClientPayment();
            return RedirectToAction("Index");
        }
    }
}