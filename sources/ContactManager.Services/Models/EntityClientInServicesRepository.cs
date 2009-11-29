using System;
using System.Linq;
using ContactManager.Models;
using ContactManager.Services.Interfaces;

namespace ContactManager.Services.Models
{
    public class EntityClientInServicesRepository : IClientInServicesRepository
    {
        private readonly AstraEntities _entities;

        #region Constructors
        public EntityClientInServicesRepository()
        {
            _entities = new AstraEntities();
        }

        public EntityClientInServicesRepository(AstraEntities entities)
        {
            _entities = entities;
        }
        #endregion

        #region IClientServiceActivitiyRepository Members

        public ClientInServices CreateActivity(ClientInServices activity)
        {
            activity.astra_Clients = _entities.ClientSet.Where(c => c.UserId == activity.ClientId).FirstOrDefault();
            activity.astra_Services = _entities.ServiceSet.Where(s => s.ServiceId == activity.ServiceId).FirstOrDefault();
            activity.aspnet_Users = _entities.ASPUserSet.Where(u => u.UserId == activity.UserId).FirstOrDefault();
            activity.Date = DateTime.Now;
            _entities.AddToClientInServicesSet(activity);
            _entities.SaveChanges();
            return activity;
        }

        public ClientInServices UpdateActivity(ClientInServices activity)
        {
            var _activity = _entities.ClientInServicesSet.Where(a => a.ActivityId == activity.ActivityId).FirstOrDefault();
            _entities.ApplyPropertyChanges(_activity.EntityKey.EntitySetName, activity);
            //_activity.EndDate = DateTime.Now;
            _entities.SaveChanges();
            return _activity;
        }

        #endregion
    }
}