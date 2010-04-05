using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.Mvc;
using ContactManager.Addresses.Interfaces;
using ContactManager.Addresses.Services;
using ContactManager.Models;
using ContactManager.Models.Validation;

namespace ContactManager.Addresses.Controllers
{
    public class StreetController : Controller
    {
        private readonly IValidationDictionary validationDictionary;
        private readonly IStreetService _streetService;

        public StreetController()
        {
            validationDictionary = new ModelStateWrapper(ModelState);
            _streetService = new StreetService(validationDictionary);
        }

        public ActionResult Index()
        {
            return View(_streetService.ListStreets());
        }


        public ActionResult Details(int id)
        {
            return View();
        }


        public ActionResult Create()
        {
            return View();
        }

        public ActionResult ListStreets()
        {
            var list = _streetService.ListStreets();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Create(Street street)
        {
            if (_streetService.CreateStreet(street))
            {
                object item = new { value = street.StreetId, name = street.Name };
                return Json(item);
            }
            return View(street);
        }


        public ActionResult Edit(int id)
        {
            return View(_streetService.GetStreet(id));
        }


        [HttpPost]
        public ActionResult Edit(Street street)
        {
            if (_streetService.EditStreet(street))
            {
                var streets = _streetService.ListStreets();
                var resultView = View("Index", streets);
                StringResult sr = new StringResult();
                sr.ViewName = resultView.ViewName;
                sr.MasterName = resultView.MasterName;
                sr.ViewData = new ViewDataDictionary(streets);
                sr.TempData = resultView.TempData;
                // let them eat cake
                sr.ExecuteResult(this.ControllerContext);

                Session["DialogData"] = sr.Html.Replace("\"", "'");
                return RedirectToAction("Index", "Address");
            }
            return View(street);
            
        }
    }

    public class StringResult : ViewResult
    {
        public string Html { get; set; }
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if (string.IsNullOrEmpty(this.ViewName))
            {
                this.ViewName =
                     context.RouteData.GetRequiredString("action");
            }
            ViewEngineResult result = null;
            if (this.View == null)
            {
                result = this.FindView(context);
                this.View = result.View;
            }
            var viewContext = new ViewContext(context, View, ViewData, TempData, null);
            using (var stream = new MemoryStream())
            using (var writer = new StreamWriter(stream))
            {
                // used to write to context.HttpContext.Response.Output
                this.View.Render(viewContext, writer);
                writer.Flush();
                Html = Encoding.UTF8.GetString(stream.ToArray());
            }
            if (result != null)
            {
                result.ViewEngine.ReleaseView(context, this.View);
            }
        }
    }
}
