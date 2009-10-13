﻿using System;
using System.Collections.Generic;
using System.Linq;
using ContactManager.Intefaces;

namespace ContactManager.Models
{
    public class EntityAccountTransactionRepository : IAccountTransactionRepository
    {
        private readonly AstraEntities _entities = new AstraEntities();

        #region Constructors
        public EntityAccountTransactionRepository()
        {
            _entities = new AstraEntities();
        }

        public EntityAccountTransactionRepository(AstraEntities entities)
        {
            _entities = entities;
        }
        #endregion

        #region IAccountTransactionRepository Members

        public List<AccountTransaction> ListTransaction()
        {
            var list = _entities.AccountTransactionSet.ToList();
            foreach (var item in list)
            {
                item.astra_ClientsReference.Load();
                item.astra_ClientsReference.Value.aspnet_UsersReference.Load();
                item.astra_ClientsReference.Value.UserName =
                    item.astra_ClientsReference.Value.aspnet_UsersReference.Value.UserName;
                item.aspnet_UsersReference.Load();
                item.astra_Account_Transactions_MethodsReference.Load();
                item.astra_ServicesReference.Load();
                item.mkt_PPPProfilesReference.Load();
            }
            return list;
        }

        public List<AccountTransaction> ListTransaction(Guid userId)
        {
            var user = _entities.ClientSet.Where(c => c.UserId == userId).FirstOrDefault();
            user.astra_Account_Transactions.Load();
            var list = user.astra_Account_Transactions.ToList();
            //var list = _entities.AccountTransactionSet.Where(t => t.astra_ClientsReference.Value.UserId == userId).ToList();
            foreach (var item in list)
            {
                item.astra_ClientsReference.Load();
                item.astra_ClientsReference.Value.aspnet_UsersReference.Load();
                item.astra_ClientsReference.Value.UserName =
                    item.astra_ClientsReference.Value.aspnet_UsersReference.Value.UserName;
                item.aspnet_UsersReference.Load();
                item.astra_Account_Transactions_MethodsReference.Load();
                item.astra_ServicesReference.Load();
                item.mkt_PPPProfilesReference.Load();
            }
            return list;
        }

        public bool CreateTransaction(AccountTransaction transaction)
        {
            transaction.astra_Clients = _entities.ClientSet.Where(c => c.UserId == transaction.ClientId).FirstOrDefault();
            transaction.aspnet_Users = _entities.ASPUserSet.Where(u => u.UserId == transaction.UserId).FirstOrDefault();
            transaction.astra_Account_Transactions_Methods = _entities.AccountTransactionMethodSet
                    .Where(m => m.MethodId == transaction.MethodId).FirstOrDefault();
            transaction.Date = DateTime.Now;
            _entities.AddToAccountTransactionSet(transaction);
            _entities.SaveChanges();
            return true;
        }

        public bool DeleteTransactions(Guid userId)
        {
            var list = _entities.AccountTransactionSet.Where(a => a.astra_Clients.UserId == userId);
            foreach (var transaction in list)
            {
                _entities.DeleteObject(transaction);
            }
            list = _entities.AccountTransactionSet.Where(a => a.aspnet_Users.UserId == userId);
            foreach (var transaction in list)
            {
                _entities.DeleteObject(transaction);
            }
            _entities.SaveChanges();
            return true;
        }

        public void ProcessClientPayment() 
        { 
            foreach(var secrets in _entities.PPPSecretSet.ToList())
            {
                secrets.mkt_PPPProfilesReference.Load();
                secrets.astra_ClientsReference.Load();
                
                var client = secrets.astra_ClientsReference.Value;
                var profile = secrets.mkt_PPPProfilesReference.Value;

                if (profile == null || 
                    client == null || 
                    !profile.Cost.HasValue ||
                    client.Status != 1 ||
                    secrets.Status != 1) continue;

                client.Balance = client.Balance - profile.Cost.Value;
                _entities.SaveChanges();

                var method = _entities.AccountTransactionMethodSet.Where(m => m.Name == "Profile").FirstOrDefault() ??
                             CreateSystemTransactionMethod("Profile", "Profile fee");
                var transaction = new AccountTransaction 
                { 
                    ClientId = client.UserId,
                    Balance = client.Balance,
                    Sum = - profile.Cost.Value,
                    MethodId = method.MethodId,
                    mkt_PPPProfiles = profile
                };
                CreateTransaction(transaction);

                client.astra_ClientsServicesActivities.Load();
                foreach (var item in client.astra_ClientsServicesActivities.Where(a => a.Active))
                {
                    item.astra_ServicesReference.Load();
                    var service = item.astra_ServicesReference.Value;
                    if (!service.IsRegular) continue;

                    client.Balance = client.Balance - service.Cost;
                    _entities.SaveChanges();

                    method = _entities.AccountTransactionMethodSet.Where(m => m.Name == "Service").FirstOrDefault() ??
                             CreateSystemTransactionMethod("Service", "Service fee");
                    transaction = new AccountTransaction
                    {
                        ClientId = client.UserId,
                        Balance = client.Balance,
                        Sum = -profile.Cost.Value,
                        MethodId = method.MethodId,
                        astra_Services = service
                    };
                    CreateTransaction(transaction);
                }

                if (client.Balance < client.Credit)
                    secrets.Status = 0;
                _entities.SaveChanges();
            }
        }

        private AccountTransactionMethod CreateSystemTransactionMethod(string name, string comment)
        {
            var method = new AccountTransactionMethod
            {
                Name = name,
                Comment = comment                
            };
            _entities.AddToAccountTransactionMethodSet(method);
            _entities.SaveChanges();
            return method;
        }

        #endregion
    }
}
