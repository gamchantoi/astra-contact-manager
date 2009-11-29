using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContactManager.Intefaces;

namespace ContactManager.Models
{
    public class EntityClientServiceActivitiyRepository : IClientServiceActivitiyRepository
    {
        private AstraEntities _entities;

        #region Constructors
        public EntityClientServiceActivitiyRepository()
        {
            _entities = new AstraEntities();
        }

        public EntityClientServiceActivitiyRepository(AstraEntities entities)
        {
            _entities = entities;
        }
        #endregion

        #region IClientServiceActivitiyRepository Members

        public ClientServiceActivitiy CreateActivity(ClientServiceActivitiy activity)
        {
            activity.astra_Clients = _entities.ClientSet.Where(c => c.UserId == activity.ClientId).FirstOrDefault();
            activity.astra_Services = _entities.ServiceSet.Where(s => s.ServiceId == activity.ServiceId).FirstOrDefault();
            activity.aspnet_Users = _entities.ASPUserSet.Where(u => u.UserId == activity.UserId).FirstOrDefault();
            activity.StartDate = DateTime.Now;
            _entities.AddToClientServiceActivitiySet(activity);
            _entities.SaveChanges();
            return activity;
        }

        public ClientServiceActivitiy UpdateActivity(ClientServiceActivitiy activity)
        {
            var _activity = _entities.ClientServiceActivitiySet.Where(a => a.ActivityId == activity.ActivityId).FirstOrDefault();
            _entities.ApplyPropertyChanges(_activity.EntityKey.EntitySetName, activity);
            _activity.EndDate = DateTime.Now;
            _entities.SaveChanges();
            return _activity;
        }

        #endregion
    }
}
