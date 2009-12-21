using System.Collections.Generic;
using System.Linq;
using ContactManager.Models;
using ContactManager.Users.Interfaces;

namespace ContactManager.Users.Models
{
    public class EntityStatusRepository : RepositoryBase<Status>, IStatusRepository
    {
        //private readonly AstraEntities _entities;

        //#region Constructors
        //public EntityStatusRepository() : this(new AstraEntities()) { }

        //public EntityStatusRepository(AstraEntities entities)
        //{
        //    _entities = entities;
        //}
        //#endregion

        public List<Status> ListStatuses()
        {
            return ObjectContext.StatusSet.ToList();
        }

        private Status CreateStatus(Status status)
        {
            ObjectContext.AddToStatusSet(status);
            ObjectContext.SaveChanges();
            return status;
        }

        public Status GetStatus(int id)
        {
            return ObjectContext.StatusSet.Where(s => s.StatusId == id).FirstOrDefault();
        }

        public Status GetStatus(Statuses status)
        {
            var _status = ObjectContext.StatusSet.Where(s => s.Name.Equals(status.ToString())).FirstOrDefault() ??
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
            ObjectContext.ApplyPropertyChanges(_status.EntityKey.EntitySetName, status);
            ObjectContext.SaveChanges();
            return _status;
        }
    }
}
