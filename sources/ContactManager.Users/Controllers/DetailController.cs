using System;
using System.Linq;
using System.Web.Mvc;
using ContactManager.Models;

namespace ContactManager.Users.Controllers
{
    public class DetailController : Controller
    {
        private readonly AstraEntities _entities = new AstraEntities();
        //
        // GET: /Detail/

        public ActionResult Index(Guid id)
        {
            var client = _entities.Clients.Where(c => c.UserId == id).FirstOrDefault();
            //client.astra_ClientsDetailsReference.Load();
            //client.aspnet_UsersReference.Load();
            //TempData["ClientForDetails"] = client;
            client.ClientDetailsReference.Load();
            if (client.ClientDetailsReference.Value == null)
                return RedirectToAction("Create", new { userId = client.UserId });
            return RedirectToAction("Edit", new { userId = client.UserId });
        }

        //
        // GET: /Detail/Create

        public ActionResult Create(Guid userId)
        {
            var client = _entities.Clients.Where(c => c.UserId == userId).FirstOrDefault();
            FillViewData(client);
            return View();
        }

        //
        // POST: /Detail/Create

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(ClientDetail detail, Address adderess, Contract contract, Guid UserId)
        {
            var client = _entities.Clients.Where(c => c.UserId == UserId).FirstOrDefault();
            try
            {
                detail.LastUpdatedDate = DateTime.Now;
                adderess.LastUpdatedDate = DateTime.Now;

                _entities.AddToClientDetailSet(detail);
                _entities.AddToAddresses(adderess);
                _entities.AddToContractSet(contract);

                client.ClientDetails = detail;
                client.Address = adderess;
                client.Contract = contract;

                _entities.SaveChanges();
                //Session.Remove("UserForDetails");
                return RedirectToAction("Edit", "User", new { id = UserId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("_FORM", ex.Message);
                FillViewData(client);
                return View();
            }
        }

        //
        // GET: /Detail/Edit/5

        public ActionResult Edit(Guid userId)
        {
            var client = _entities.Clients.Where(c => c.UserId == userId).FirstOrDefault();
            client.LoadDetailsReferences();
            //var client = TempData["ClientForDetails"] as Client;
            return View(client);
        }

        //
        // POST: /Detail/Edit/5

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(ClientDetail detail, Address adderess, Contract contract, Guid UserId)
        {
            var client = _entities.Clients.Where(c => c.UserId == UserId).FirstOrDefault();
            client.LoadDetailsReferences();
            try
            {
                //var det = _entities.ClientDetailSet.Where(d => d.DetailId == detail.DetailId).FirstOrDefault();
                if (client.ClientDetailsReference.Value == null)
                {
                    _entities.AddToClientDetailSet(detail);
                    client.ClientDetails = detail;
                }
                else
                {
                    _entities.ApplyPropertyChanges(client.ClientDetailsReference.Value.EntityKey.EntitySetName, detail);
                }
                client.ClientDetailsReference.Value.LastUpdatedDate = DateTime.Now;
                _entities.SaveChanges();

                //var addr = _entities.AddressSet.Where(a => a.AddressId == adderess.AddressId).FirstOrDefault();
                if (client.Address == null)
                {
                    _entities.AddToAddresses(adderess);
                    client.Address = adderess;

                }
                else
                {
                    _entities.ApplyPropertyChanges(client.AddressReference.Value.EntityKey.EntitySetName, adderess);
                }
                client.AddressReference.Value.LastUpdatedDate = DateTime.Now;
                _entities.SaveChanges();

                //var contr = _entities.ContractSet.Where(c => c.ContractId == contract.ContractId).FirstOrDefault();
                if (client.ContractReference.Value == null)
                {
                    _entities.AddToContractSet(contract);
                    client.Contract = contract;
                }
                else
                {
                    _entities.ApplyPropertyChanges(client.ContractReference.Value.EntityKey.EntitySetName, contract);
                }

                _entities.SaveChanges();
                return RedirectToAction("Edit", "User", new { id = UserId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("_FORM", ex.Message);
                client.LoadDetailsReferences();
                return View(client);
            }
        }

        private void FillViewData(Client client)
        {
            ViewData["UserId"] = client.UserId;
            ViewData["ClientName"] = client.UserName;
        }
    }
}