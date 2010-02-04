using System;
using System.Web.Mvc;
using ContactManager.Models;
using ContactManager.Models.Validation;
using ContactManager.Users.Interfaces;
using ContactManager.Users.Services;

namespace ContactManager.Users.Controllers
{
    public class ContractController : Controller
    {
        private ContractService _contractService;
        private readonly IValidationDictionary _validationDictionary;
        private readonly IUserFacade _userFasade;

        public ContractController()
        {
            _validationDictionary = new ModelStateWrapper(ModelState);
            _contractService = new ContractService(_validationDictionary);
            _userFasade = new UserFacade(_validationDictionary);
        }

        public ActionResult Index()
        {
            return View(_contractService.ListContracts());
        }


        public ActionResult Create(Guid id)
        {
            ViewData["UserId"] = id;
            return View();
        } 


        [HttpPost]
        public ActionResult Create(Contract contract)
        {
            _contractService.CreateContract(contract);
            return RedirectToAction("Edit", "User", new { id = contract.UserId, area = "Users" }); 
        }


        public ActionResult Edit(Guid id)
        {
            ViewData["UserId"] = id;
            return View(_contractService.GetContract(id));
        }


        [HttpPost]
        public ActionResult Edit(Contract contract)
        {
            _contractService.EditContract(contract);
            return RedirectToAction("Edit", "User", new { id = contract.UserId, area = "Users" }); 
        }
    }
}
