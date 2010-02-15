using System;
using System.Collections.Generic;
using System.Linq;
using ContactManager.Models;

namespace ContactManager.Addresses.Models
{
    public class EntityAddressRepository : RepositoryBase<Address>
    {
        public List<Address> ListAddresses()
        {
            return ObjectContext.Addresses.ToList();
        }

        public void Create(Address address)
        {
            ObjectContext.AddToAddresses(address);
            address.LastUpdatedDate = DateTime.Now;
            ObjectContext.SaveChanges();
            
        }

        public Address GetAddress(int id)
        {
            return (from m in ObjectContext.Addresses where id == m.AddressId select m).FirstOrDefault();
        }

        public Address Edit(Address address)
        {
            var _address = GetAddress(address.AddressId);
            
            var _street =
                (from m in ObjectContext.Streets where m.StreetId == address.Street.StreetId select m).FirstOrDefault();
            _address.Street = _street;
            ObjectContext.ApplyPropertyChanges(_address.EntityKey.EntitySetName, address);
            _address.LastUpdatedDate = DateTime.Now;
            ObjectContext.SaveChanges();
            return _address;
        }

        public Address GetAddress(Guid id)
        {
            var client = ObjectContext.Clients.FirstOrDefault(m => m.UserId == id);
            client.AddressReference.Load();

            return client.Address;
        }
    }
}
