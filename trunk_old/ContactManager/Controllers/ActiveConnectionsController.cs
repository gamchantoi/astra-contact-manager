using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using ContactManager.Models;

namespace ContactManager.Controllers
{
    public class ActiveConnectionsController : BaseController
    {
        EntitySSHRepository _repository = new EntitySSHRepository();
        private AstraEntities _astraEntities = new AstraEntities();
        //
        // GET: /ActiveConnections/

        public ActionResult Index()
        {
            var host = GetCurrentHost();
            _repository.Connect(host.Address, host.UserName, host.UserPassword);
            var conn = _repository.ListActiveConnections();
            _repository.Disconnect();
            return View(conn);
        }

        public Host GetCurrentHost()
        {
            var hostId = int.Parse(HttpContext.Profile.GetPropertyValue("HostId").ToString());
            return _astraEntities.HostSet.Where(h => h.HostId == hostId).FirstOrDefault();
        }

    }
}
