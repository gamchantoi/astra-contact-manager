using System.Collections.Generic;
using System.Linq;
using ContactManager.Models;
using ContactManager.Models.Enums;
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
            return ObjectContext.Statuses.ToList();
        }

        private Status CreateStatus(Status status)
        {
            ObjectContext.AddToStatuses(status);
            ObjectContext.SaveChanges();
            return status;
        }

        public Status GetStatus(int id)
        {
            return ObjectContext.Statuses.Where(s => s.StatusId == id).FirstOrDefault();
        }

        public Status GetStatus(STATUSES status)
        {
            var str = status.ToString();
            var _status = ObjectContext.Statuses.Where(s => s.Name.Equals(str)).FirstOrDefault() ??
                         CreateStatus(new Status
                                          {
                                              DisplayName = str,
                                              Name = str
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
