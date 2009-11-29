using System.Linq;
using ContactManager.Models;
using ContactManager.Services.Interfaces;

namespace ContactManager.Services.Models
{
    public class EntitySystemServicesRepository : ISystemServicesRepository
    {
        private readonly AstraEntities _entities;

        #region Constructors
        public EntitySystemServicesRepository()
        {
            _entities = new AstraEntities();
        }

        public EntitySystemServicesRepository(AstraEntities entities)
        {
            _entities = entities;
        }
        #endregion

        #region ISystemServicesRepository Members

        public SystemService GetService(string name)
        {
            var service = _entities.SystemServiceSet.Where(s => s.Name.Equals(name)).FirstOrDefault();
            if (service == null)
            {
                var _service = new SystemService();
                _service.Name = name;
                _entities.AddToSystemServiceSet(_service);
                _entities.SaveChanges();
                service = _service;
            }
            return service;
        }

        public bool RegisterService(SystemService system, int serviceId)
        {
            system.astra_Services.Add(_entities.ServiceSet.Where(s => s.ServiceId == serviceId).FirstOrDefault());
            _entities.SaveChanges();
            return true;
        }

        #endregion
    }
}