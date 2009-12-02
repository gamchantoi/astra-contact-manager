using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContactManager.Hosts.Interfaces;
using ContactManager.Models;

namespace ContactManager.Hosts.Models
{
    public class HostsRepository : IHostsRepository
    {
        private AstraEntities _entities;

        #region IHostsRepository Constructors

        public HostsRepository()
        {
            _entities = new AstraEntities();
        }

        public HostsRepository (AstraEntities entities)
        {
            _entities = entities;
        }

        #endregion

        #region IHostsRepository CreateHost(Host host)

        public Host CreateHost(Host host)
        {
            host.LastUpdatedDate = DateTime.Now;
            _entities.AddToHostSet(host);
            _entities.SaveChanges();
            return host;
        }

        #endregion

        #region IHostsRepository DeleteHost(int id)
        public void DeleteHost(int id)
        {
            var host = _entities.HostSet.Where(h => h.HostId == id).FirstOrDefault();
            _entities.DeleteObject(host);
            _entities.SaveChanges();
        }
        #endregion

        #region IHostsRepository EditHost(Host host)
        public Host EditHost(Host host)
        {
            var _host = _entities.HostSet.Where(ho => ho.HostId == host.HostId).FirstOrDefault();
            _entities.ApplyPropertyChanges(_host.EntityKey.EntitySetName, host);
            //----------------------------------------------------
            _host.LastUpdatedDate = DateTime.Now;
            _entities.SaveChanges();
            return host;
        }
        #endregion

        #region IHostsRepository GetHost(int id)
        public Host GetHost(int id)
        {
            return _entities.HostSet.Where(h => h.HostId == id).First();
        }
        #endregion

        #region IHostsRepository IEnumerable<Host> ListHosts()
        public IEnumerable<Host> ListHosts()
        {
            return _entities.HostSet.ToList();
        }

        #endregion
    }
}
