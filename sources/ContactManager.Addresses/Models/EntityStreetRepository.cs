using System.Collections.Generic;
using System.Linq;
using ContactManager.Models;

namespace ContactManager.Addresses.Models
{
    public class EntityStreetRepository : RepositoryBase<Street>
    {
        public List<Street> ListStreets()
        {
            return ObjectContext.Streets.ToList();
        }

        public Street GetStreet(int id)
        {
            var street = (from m in ObjectContext.Streets where id == m.StreetId select m).FirstOrDefault();
            return street;
        }

        public Street Edit(Street street)
        {
            var _street = GetStreet(street.StreetId);
            ObjectContext.ApplyPropertyChanges(_street.EntityKey.EntitySetName, street);
            ObjectContext.SaveChanges();
            return street;
        }
    }
}
