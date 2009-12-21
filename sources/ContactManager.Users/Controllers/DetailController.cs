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
            client.astra_ClientsDetailsReference.Load();
            if (client.astra_ClientsDetailsReference.Value == null)
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
                _entities.AddToAddressSet(adderess);
                _entities.AddToContractSet(contract);

                client.astra_ClientsDetails = detail;
                client.astra_Addresses = adderess;
                client.astra_Contracts = contract;

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
                if (client.astra_ClientsDetailsReference.Value == null)
                {
                    _entities.AddToClientDetailSet(detail);
                    client.astra_ClientsDetails = detail;
                }
                else
                {
                    _entities.ApplyPropertyChanges(client.astra_ClientsDetailsReference.Value.EntityKey.EntitySetName, detail);
                }
                client.astra_ClientsDetailsReference.Value.LastUpdatedDate = DateTime.Now;
                _entities.SaveChanges();

                //var addr = _entities.AddressSet.Where(a => a.AddressId == adderess.AddressId).FirstOrDefault();
                if (client.astra_AddressesReference.Value == null)
                {
                    _entities.AddToAddressSet(adderess);
                    client.astra_Addresses = adderess;

                }
                else
                {
                    _entities.ApplyPropertyChanges(client.astra_AddressesReference.Value.EntityKey.EntitySetName, adderess);
                }
                client.astra_AddressesReference.Value.LastUpdatedDate = DateTime.Now;
                _entities.SaveChanges();

                //var contr = _entities.ContractSet.Where(c => c.ContractId == contract.ContractId).FirstOrDefault();
                if (client.astra_ContractsReference.Value == null)
                {
                    _entities.AddToContractSet(contract);
                    client.astra_Contracts = contract;
                }
                else
                {
                    _entities.ApplyPropertyChanges(client.astra_ContractsReference.Value.EntityKey.EntitySetName, contract);
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