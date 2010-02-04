using System;
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
        private readonly IUserFacade _userFasade;

        public DetailController()
        {
            _validationDictionary = new ModelStateWrapper(ModelState);
            _detailService = new DetailService(_validationDictionary);
            _userFasade = new UserFacade(_validationDictionary);
        }

        public ActionResult Index(Guid id)
        {
            return View(_detailService.ListDetails());
        }

        public ActionResult Create(Guid id)
        {
            ViewData["UserId"] = id;
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(ClientDetail detail)
        {
            _detailService.CreateDetail(detail);
            return RedirectToAction("Edit", "User", new { id = detail.UserId, area = "Users" });
        }

        public ActionResult Edit(Guid id)
        {
            return View(_detailService.GetDetails(id));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(ClientDetail detail)
        {
            _detailService.EditDetail(detail);
            return RedirectToAction("Edit", "User", new { id = detail.UserId, area = "Users" });
        }
    }
}