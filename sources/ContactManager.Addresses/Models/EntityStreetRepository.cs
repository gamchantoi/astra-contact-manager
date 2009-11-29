using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContactManager.Models;

namespace ContactManager.Addresses.Models
{
    public class EntityStreetRepository
    {
        private readonly AstraEntities _entities;

        #region Constructors
        public EntityStreetRepository() : this(new AstraEntities()) { }

        public EntityStreetRepository(AstraEntities entities)
        {
            _entities = entities;
        }
        #endregion

        public List<Street> ListStreets()
        {
            return _entities.StreetSet.ToList();
        }
    }
}
