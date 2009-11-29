using System.Collections.Generic;
using System.Linq;
using ContactManager.Models;
using ContactManager.Users.Interfaces;

namespace ContactManager.Users.Models
{
    public class EntityStatusRepository : IStatusRepository
    {
        private readonly AstraEntities _entities;

        #region Constructors
        public EntityStatusRepository() : this(new AstraEntities()) { }

        public EntityStatusRepository(AstraEntities entities)
        {
            _entities = entities;
        }
        #endregion

        public List<Status> ListStatuses()
        {
            return _entities.StatusSet.ToList();
        }

        private Status CreateStatus(Status status)
        {
            _entities.AddToStatusSet(status);
            _entities.SaveChanges();
            return status;
        }

        public Status GetStatus(int id)
        {
            return _entities.StatusSet.Where(s => s.StatusId == id).FirstOrDefault();
        }

        public Status GetStatus(Statuses status)
        {
            var _status = _entities.StatusSet.Where(s => s.Name.Equals(status.ToString())).FirstOrDefault() ??
                         CreateStatus(new Status
                                          {
                                              DisplayName = status.ToString(),
                                              Name = status.ToString()
                                          });
            return _status;
        }

        public Status EditStatus(Status status)
        {
            var _status = GetStatus(status.StatusId);
            status.Name = _status.Name;
            _entities.ApplyPropertyChanges(_status.EntityKey.EntitySetName, status);
            _entities.SaveChanges();
            return _status;
        }
    }
}
