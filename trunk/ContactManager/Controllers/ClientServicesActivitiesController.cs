using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace ContactManager.Controllers
{
    public class ClientServicesActivitiesController : Controller
    {
        //
        // GET: /ClientServicesActivities/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /ClientServicesActivities/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /ClientServicesActivities/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /ClientServicesActivities/Create

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
        // GET: /ClientServicesActivities/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /ClientServicesActivities/Edit/5

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
    }
}
