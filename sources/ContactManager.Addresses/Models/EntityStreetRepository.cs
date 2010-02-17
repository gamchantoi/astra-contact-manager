using System.Collections.Generic;
using System.Linq;
using ContactManager.Models;
using ContactManager.Addresses.Interfaces;

namespace ContactManager.Addresses.Models
{
    public class EntityStreetRepository : RepositoryBase<Street> , IStreetRepository
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

        public Street EditStreet(Street street)
        {
            var _street = GetStreet(street.StreetId);
            ObjectContext.ApplyPropertyChanges(_street.EntityKey.EntitySetName, street);
            ObjectContext.SaveChanges();
            return street;
        }

        public Street CreateStreet(Street street)
        {
            ObjectContext.AddToStreets(street);
            ObjectContext.SaveChanges();
            return street;
        }

        public Street GetStreet(string name)
        {
            var street = (from m in ObjectContext.Streets where name == m.Name select m).FirstOrDefault();
            return street;
        }
    }
}
