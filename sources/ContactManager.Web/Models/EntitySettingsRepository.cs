using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContactManager.Models;
using ContactManager.Models.Enums;
using ContactManager.Models.Validation;
using ContactManager.Users.Interfaces;
using ContactManager.Users.Services;

namespace ContactManager.Web.Models
{
    public class EntitySettingsRepository:RepositoryBase<User>
    {
        private IValidationDictionary _validationDictionary;
        private IUserFasade _fasade;

        public EntitySettingsRepository(IValidationDictionary validationDictionary)
        {
            _validationDictionary = validationDictionary;
            _fasade = new UserFasade(validationDictionary);
        }

        public void ClearDB()
        {
            foreach (var message in ObjectContext.Messages.ToList())
            {
                ObjectContext.DeleteObject(message);
            }
            foreach (var messageType in ObjectContext.MessageTypes.ToList())
            {
                ObjectContext.DeleteObject(messageType);
            }

            foreach (var transaction in ObjectContext.Transactions.ToList())
            {
                ObjectContext.DeleteObject(transaction);
            }
            foreach (var paymentMethod in ObjectContext.PaymentMethods.ToList())
            {
                ObjectContext.DeleteObject(paymentMethod);
            }
            foreach (var clientInServicesSet in ObjectContext.ClientInServicesSet.ToList())
            {
                ObjectContext.DeleteObject(clientInServicesSet);
            }
            foreach (var servicesSet in ObjectContext.ServiceSet.ToList())
            {
                ObjectContext.DeleteObject(servicesSet);
            }

            foreach (var pPPSecrets in ObjectContext.PPPSecrets.ToList())
            {
                ObjectContext.DeleteObject(pPPSecrets);
            }
            foreach (var profileSet in ObjectContext.ProfileSet.ToList())
            {
                ObjectContext.DeleteObject(profileSet);
            }
            foreach (var poolSet in ObjectContext.PoolSet.ToList())
            {
                ObjectContext.DeleteObject(poolSet);
            }

            var users = _fasade.UserService.ListUsers(ROLES.client.ToString());

            foreach (var user in users)
            {
                var client = _fasade.ClientService.GetClient(user.UserId);
                if(client!=null)    
                ObjectContext.DeleteObject(client);
            }
            ObjectContext.SaveChanges();

            foreach (var user in users)
            {
                _fasade.UserService.DeleteUser(user.UserId);
            }


            foreach (var hostSet in ObjectContext.HostSet.ToList())
            {
                ObjectContext.DeleteObject(hostSet);
            }
            foreach (var contractSet in ObjectContext.ContractSet.ToList())
            {
                ObjectContext.DeleteObject(contractSet);
            }
            foreach (var clientDetailSet in ObjectContext.ClientDetailSet.ToList())
            {
                ObjectContext.DeleteObject(clientDetailSet);
            }
            foreach (var address in ObjectContext.Addresses.ToList())
            {
                ObjectContext.DeleteObject(address);
            }
            foreach (var street in ObjectContext.Streets.ToList())
            {
                ObjectContext.DeleteObject(street);
            }
            //foreach (var client in ObjectContext.Clients.ToList())
            //{
            //    ObjectContext.DeleteObject(client);
            //}
            ObjectContext.SaveChanges();
        }
    }
}
