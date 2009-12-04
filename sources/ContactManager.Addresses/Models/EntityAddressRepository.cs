using System;
using System.Collections.Generic;
using System.Linq;
using ContactManager.Models;

namespace ContactManager.Addresses.Models
{
    public class EntityAddressRepository
    {
        private readonly AstraEntities _entities;

        #region Constructors
        public EntityAddressRepository() : this(new AstraEntities()) { }

        public EntityAddressRepository(AstraEntities entities)
        {
            _entities = entities;
        }
        #endregion

        public List<Address> ListAddresses()
        {
            return _entities.AddressSet.ToList();
        }

        public Address Create(Address address)
        {

            _entities.AddToAddressSet(address);
            address.LastUpdatedDate = DateTime.Now;
            _entities.SaveChanges();
            return address;
        }

        public Address GetAddress(int id)
        {
            return (from m in _entities.AddressSet where id == m.AddressId select m).FirstOrDefault();
        }

        public Address Edit(Address address)
        {
            Address _address = GetAddress(address.AddressId);
            var _street =
                (from m in _entities.StreetSet where m.StreetId == address.Street.StreetId select m).FirstOrDefault();
            _address.astra_Streets = _street;
            _entities.ApplyPropertyChanges(_address.EntityKey.EntitySetName, address);
            _address.LastUpdatedDate = DateTime.Now;
            _entities.SaveChanges();
            return _address;
        }
    }
}
