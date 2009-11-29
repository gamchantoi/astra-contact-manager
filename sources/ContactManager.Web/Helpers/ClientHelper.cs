using System;
using System.Linq;
using ContactManager.Models;
using ContactManager.Intefaces;

namespace ContactManager.Helpers
{
    public class ClientHelper : IClientHelper
    {
        #region IClientUtil Members

        public Client CreateClient(IUser user)
        {
            var client = new Client
                             {
                                 Status = user.Status,
                                 Balance = user.Balance,
                                 LocalAddress = user.LocalAddress,
                                 RemoteAddress = user.RemoteAddress,
                                 astra_ClientsDetails = CreateClientDetails(user),
                                 astra_Tariffs = BaseEntity.Entity.TariffSet.Where(t => t.TariffId == user.TariffId).First(),
                                 aspnet_Users = BaseEntity.Entity.ASPUserSet.Where(u => u.UserId == user.UserId).First(),
                                 LastUpdatedDate = DateTime.Now
                             };
            return client;
        }

        public ClientDetail CreateClientDetails(IUser user)
        {
            if (!ValidateClientDetail(user))
                return null;
            var detail = new ClientDetail
                             {
                                 FirstName = user.FirstName,
                                 MiddleName = user.MiddleName,
                                 LastName = user.LastName,
                                 LastUpdatedDate = DateTime.Now
                             };
            return detail;
        }

        public Client UpdateClient(IUser user)
        {
            Client client;
            var aUser = BaseEntity.Entity.ASPUserSet.Where(u => u.UserId == user.UserId).First();
            aUser.astra_ClientsReference.Load();

            if (aUser.astra_ClientsReference.Value == null)
            {
                client = CreateClient(user);
            }
            else
            {
                client = aUser.astra_ClientsReference.Value;
                client.Status = user.Status;
                client.Balance = user.Balance;
                client.LocalAddress = user.LocalAddress;
                client.RemoteAddress = user.RemoteAddress;
            }
            return client;
        }

        public ClientDetail UpdateClientDetails(IUser user)
        {
            var aUser = BaseEntity.Entity.ASPUserSet.Where(u => u.UserId == user.UserId).First();
            aUser.astra_ClientsReference.Load();
            
            var client = aUser.astra_ClientsReference.Value ?? CreateClient(user);
            client.astra_ClientsDetailsReference.Load();
            
            var detail = client.astra_ClientsDetailsReference.Value;
            if(detail == null)
            {
                detail = CreateClientDetails(user);
                client.astra_ClientsDetails = detail;
            }
            else
            {
                if(!string.IsNullOrEmpty(user.FirstName))
                    detail.FirstName = user.FirstName;
                if (!string.IsNullOrEmpty(user.MiddleName))
                    detail.MiddleName = user.MiddleName;
                if (!string.IsNullOrEmpty(user.LastName))
                    detail.LastName = user.LastName;
                detail.LastUpdatedDate = DateTime.Now;
            }
            return detail;
        }

        public Client GetClient(Guid userId)
        {
            var user = BaseEntity.Entity.ASPUserSet.Where(u => u.UserId == userId).First();
            user.astra_ClientsReference.Load();
            return user.astra_ClientsReference.Value ?? new Client();
        }

        public ClientDetail GetClientDetails(Guid userId)
        {
            var client = GetClient(userId);
            if (client.EntityKey != null)
                client.astra_ClientsDetailsReference.Load();
            return client.astra_ClientsDetailsReference.Value ?? new ClientDetail();
        }

        private bool ValidateClientDetail(IUser user) 
        {
            if (String.IsNullOrEmpty(user.FirstName) &&
                String.IsNullOrEmpty(user.LastName) &&
                String.IsNullOrEmpty(user.MiddleName))
                return false;
            return true;
        }

        #endregion
    }
}