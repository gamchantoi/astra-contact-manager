using System;
using System.Collections.Generic;
using System.Linq;
using ContactManager.Accounts.Interfaces;
using ContactManager.Models;
using ContactManager.Models.ViewModels;

namespace ContactManager.Accounts.Models
{
    public class EntityTransactionRepository : ITransactionRepository
    {
        #region Constructors
        public EntityTransactionRepository()
        {
            Entities = new AstraEntities();
        }

        public EntityTransactionRepository(AstraEntities entities)
        {
            Entities = entities;
        }
        #endregion

        #region IAccountTransactionRepository Members

        public Transaction GetTransaction(LoadMoneyViewModel model)
        {
            return new Transaction
            {
                Sum = model.Sum,
                Comment = model.Comment,
                Balance = model.Balance
            };
        }

        public AstraEntities Entities { get; private set; }

        public List<Transaction> ListTransaction()
        {
            var list = Entities.TransactionSet.ToList();
            foreach (var item in list)
            {
                item.astra_ClientsReference.Load();
                item.Client = item.astra_ClientsReference.Value;
                item.aspnet_UsersReference.Load();
                item.User = item.aspnet_UsersReference.Value;
            }
            return list;
        }

        public List<Transaction> ListTransaction(Guid userId)
        {
            var user = Entities.ClientSet.Where(c => c.UserId == userId).FirstOrDefault();
            user.acc_Transactions.Load();
            var list = user.acc_Transactions.ToList();
            return list;
        }

        public bool CreateTransaction(Transaction transaction)
        {
            //transaction.astra_Clients = _entities.ClientSet.Where(c => c.UserId == transaction.ClientId).FirstOrDefault();
            //transaction.aspnet_Users = _entities.ASPUserSet.Where(u => u.UserId == transaction.UserId).FirstOrDefault();
            //transaction.astra_Account_Transactions_Methods = _entities.AccountTransactionMethodSet
            //    .Where(m => m.MethodId == transaction.MethodId).FirstOrDefault();
            //if (transaction.ServiceId.HasValue)
            //    transaction.astra_Services =
            //        _entities.ServiceSet.Where(s => s.ServiceId == transaction.ServiceId.Value).FirstOrDefault();
            //transaction.User = _entities.ASPUserSet.Where(u => u.UserId == transaction.Client.UserId).FirstOrDefault();
            //transaction.aspnet_UsersReference.Load();
            //transaction.aspnet_Users = Entities.ASPUserSet.Where(u => u.UserId == transaction.Client.UserId).FirstOrDefault();
            transaction.astra_Clients = Entities.ClientSet.Where(c => c.UserId == transaction.Client.UserId).FirstOrDefault();
            transaction.Date = DateTime.Now;
            Entities.AddToTransactionSet(transaction);
            Entities.SaveChanges();
            return true;
        }

        public bool DeleteTransactions(Guid userId)
        {
            var list = Entities.TransactionSet.Where(a => a.astra_Clients.UserId == userId);
            foreach (var transaction in list)
            {
                Entities.DeleteObject(transaction);
            }
            list = Entities.TransactionSet.Where(a => a.aspnet_Users.UserId == userId);
            foreach (var transaction in list)
            {
                Entities.DeleteObject(transaction);
            }
            Entities.SaveChanges();
            return true;
        }



        public void ProcessClientPayment()
        {
            //foreach(var secrets in _entities.PPPSecretSet.ToList())
            //{
            //    secrets.mkt_PPPProfilesReference.Load();
            //    secrets.astra_ClientsReference.Load();

            //    var client = secrets.astra_ClientsReference.Value;
            //    var profile = secrets.mkt_PPPProfilesReference.Value;

            //    if (profile == null || 
            //        client == null || 
            //        !profile.Cost.HasValue ||
            //        client.Status != 1 ||
            //        secrets.Status != 1) continue;

            //    client.Balance = client.Balance - profile.Cost.Value;
            //    _entities.SaveChanges();

            //    var method = _entities.AccountTransactionMethodSet.Where(m => m.Name == "Profile").FirstOrDefault() ??
            //                 CreateSystemTransactionMethod("Profile", "Profile fee");
            //    var transaction = new AccountTransaction 
            //                          { 
            //                              ClientId = client.UserId,
            //                              Balance = client.Balance,
            //                              Sum = - profile.Cost.Value,
            //                              MethodId = method.MethodId,
            //                              mkt_PPPProfiles = profile
            //                          };
            //    CreateTransaction(transaction);

            //    client.astra_ClientsServicesActivities.Load();
            //    foreach (var item in client.astra_ClientsServicesActivities.Where(a => a.Active))
            //    {
            //        item.astra_ServicesReference.Load();
            //        var service = item.astra_ServicesReference.Value;
            //        if (!service.IsRegular) continue;

            //        client.Balance = client.Balance - service.Cost;
            //        _entities.SaveChanges();

            //        method = _entities.AccountTransactionMethodSet.Where(m => m.Name == "Service").FirstOrDefault() ??
            //                 CreateSystemTransactionMethod("Service", "Service fee");
            //        transaction = new AccountTransaction
            //                          {
            //                              ClientId = client.UserId,
            //                              Balance = client.Balance,
            //                              Sum = -service.Cost,
            //                              MethodId = method.MethodId,
            //                              astra_Services = service
            //                          };
            //        CreateTransaction(transaction);
            //    }

            //    if (client.Balance < client.Credit)
            //        secrets.Status = 0;
            //    _entities.SaveChanges();
            //}
        }

        //private AccountTransactionMethod CreateSystemTransactionMethod(string name, string comment)
        //{
        //    var method = new AccountTransactionMethod
        //                     {
        //                         Name = name,
        //                         Comment = comment                
        //                     };
        //    _entities.AddToAccountTransactionMethodSet(method);
        //    _entities.SaveChanges();
        //    return method;
        //}

        #endregion
    }
}