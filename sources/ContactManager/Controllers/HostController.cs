using System;
using System.Linq;
using System.Web.Mvc;
using ContactManager.Controllers;
using ContactManager.Models;

namespace ContactManager.Controllers
{
    [HandleError]
    public class HostController : BaseController
    {
        private AstraEntities _entities = new AstraEntities();
        //
        // GET: /Host/
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {

            return View(_entities.HostSet.ToList());
        }

        //
        // GET: /Host/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Host/Create
        [Authorize(Roles = "admin")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([Bind(Exclude = "HostId")] Host h)
        {
            try
            {
                h.LastUpdatedDate = DateTime.Now;
                _entities.AddToHostSet(h);
                _entities.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("_FORM", ex.Message);
                return View();
            }
        }

        //
        // GET: /Host/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int id)
        {
            return View(_entities.HostSet.Where(h => h.HostId == id).First());
        }

        //
        // POST: /Host/Edit/5
        [Authorize(Roles = "admin")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(Host h)
        {
            try
            {
                var host = _entities.HostSet.Where(ho => ho.HostId == h.HostId).FirstOrDefault();                
                _entities.ApplyPropertyChanges(host.EntityKey.EntitySetName, h);
                host.LastUpdatedDate = DateTime.Now;
                _entities.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "admin")]
        public ActionResult Select(int id) 
        {
            HttpContext.Profile.SetPropertyValue("HostId", id);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            var host = _entities.HostSet.Where(h => h.HostId == id).FirstOrDefault();
            _entities.DeleteObject(host);
            _entities.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}