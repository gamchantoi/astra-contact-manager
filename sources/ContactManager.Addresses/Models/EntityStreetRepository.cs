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

        public Street GetStreet(int id)
        {
            Street street = (from m in _entities.StreetSet where id == m.StreetId select m).FirstOrDefault();
            return street;
        }

        public Street Edit(Street street)
        {
            Street _street = GetStreet(street.StreetId);
            _entities.ApplyPropertyChanges(_street.EntityKey.EntitySetName, street);
            _entities.SaveChanges();
            return street;
        }
    }
}
