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
        //private readonly IMembershipService _mservice;

        public TransactionController() 
        {
            IValidationDictionary validationDictionary = new ModelStateWrapper(ModelState);
            _service = new TransactionService(validationDictionary);
            //_mservice = new AccountMembershipService();
        }

        [Authorize]
        public ActionResult Index()
        {
            var _userHelper = new UserHelper();
            //var user = _mservice.GetCurrentUser();
            var isAdmin = _userHelper.IsUserInRole("admin");
            ViewData["IsAdmin"] = isAdmin;

            List<Transaction> list;
            //if (isAdmin)
                list = _service.ListTransactions();
            //list = _service.ListTransactions(_userHelper.CurrentUserId);

            Mapper.CreateMap<Transaction, TransactionVewModel>();
            var viewModelList = Mapper.Map<IList<Transaction>, IList<TransactionVewModel>>(list);
            
            return View(viewModelList);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Details(int id)
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(FormCollection collection)
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
        // GET: /AccountTransactions/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /AccountTransactions/Edit/5
        [Authorize(Roles = "admin")]
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

        //
        // GET: /AccountTransactions/Create
        [Authorize(Roles = "admin")]
        public ActionResult CreateTransactionMethod()
        {
            return View();
        }

        //
        // POST: /CreateTransactionMethod/Create
        //[Authorize(Roles = "admin")]
        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult CreateTransactionMethod(AccountTransactionMethod method)
        //{
        //    var _entities = new AstraEntities();
        //    try
        //    {
        //        _entities.AddToAccountTransactionMethodSet(method);
        //        _entities.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        [Authorize(Roles = "admin")]
        public ActionResult ProcessClientPayment()
        {
            _service.ProcessClientPayment();
            return RedirectToAction("Index");
        }
    }
}