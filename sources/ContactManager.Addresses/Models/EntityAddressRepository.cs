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
    }
}
