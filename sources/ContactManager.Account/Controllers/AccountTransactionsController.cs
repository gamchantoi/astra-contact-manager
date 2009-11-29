using System.Web.Mvc;
using ContactManager.Account.Helpers;
using ContactManager.Account.Interfaces;
using ContactManager.Account.Models;
using ContactManager.Models;
using ContactManager.Models.Validation;

namespace ContactManager.Account.Controllers
{
    public class AccountTransactionsController : Controller
    {        
        private readonly IAccountTransactionService _service;
        //private readonly IMembershipService _mservice;

        public AccountTransactionsController() 
        {
            IValidationDictionary validationDictionary = new ModelStateWrapper(ModelState);
            _service = new AccountTransactionService(validationDictionary);
            //_mservice = new AccountMembershipService();
        }

        //
        // GET: /AccountTransactions/
        [Authorize]
        public ActionResult Index()
        {
            var _userHelper = new UserHelper();
            //var user = _mservice.GetCurrentUser();
            var isAdmin = _userHelper.IsUserInRole("admin");
            ViewData["IsAdmin"] = isAdmin;
            if (isAdmin)
                return View(_service.ListTransactions());
            return View(_service.ListTransactions(_userHelper.CurrentUserId));
        }

        //
        // GET: /AccountTransactions/Details/5
        [Authorize(Roles = "admin")]
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /AccountTransactions/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /AccountTransactions/Create
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