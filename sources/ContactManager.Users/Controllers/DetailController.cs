using System;
using System.Linq;
using System.Web.Mvc;
using ContactManager.Models;
using ContactManager.Models.Validation;
using ContactManager.Users.Interfaces;
using ContactManager.Users.Services;

namespace ContactManager.Users.Controllers
{
    public class DetailController : Controller
    {
        private readonly DetailService _detailService;
        private readonly IValidationDictionary _validationDictionary;
        private readonly IUserFasade _userFasade;

        public DetailController()
        {
            _validationDictionary = new ModelStateWrapper(ModelState);
            _detailService = new DetailService(_validationDictionary);
            _userFasade = new UserFasade(_validationDictionary);
        }

        public ActionResult Index(Guid id)
        {
            return View(_detailService.ListDetails());
        }


        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult Create(ClientDetail detail)
        //{
        //    if (_detailService.CreateDetail(detail))
        //        return View("Index");
        //    return View(detail);
        //}

        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return View("Create");
            }
            return View(_detailService.GetDetails(id));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(ClientDetail detail)
        {
            if (_detailService.EditDetail(detail))
                return RedirectToAction("Index", "User");
            return View(detail);
        }
    }
}