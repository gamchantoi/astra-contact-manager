using System;
using System.Collections.Generic;
using System.Linq;
using ContactManager.Accounts.Interfaces;
using ContactManager.Models;
using ContactManager.Models.ViewModels;

namespace ContactManager.Accounts.Models
{
    public class EntityTransactionRepository : RepositoryBase<Transaction>, ITransactionRepository
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
            var list = ObjectContext.TransactionSet.ToList();
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
            var user = ObjectContext.ClientSet.Where(c => c.UserId == userId).FirstOrDefault();
            user.acc_Transactions.Load();
            var list = user.acc_Transactions.ToList();
            return list;
        }

        public bool CreateTransaction(Transaction transaction)
        {
            transaction.Date = DateTime.Now;
            transaction.aspnet_Users = ObjectContext.ASPUserSet.
                Where(u => u.UserId == transaction.aspnet_Users.UserId).FirstOrDefault();
            ObjectContext.AddToTransactionSet(transaction);
            ObjectContext.SaveChanges();
            return true;
        }

        public override void Add(Transaction transaction)
        {
            
                transaction.Date = DateTime.Now;
                base.Add(transaction);
                base.SaveAllObjectChanges();
            
        }

        public bool DeleteTransactions(Guid userId)
        {
            var list = ObjectContext.TransactionSet.Where(a => a.astra_Clients.UserId == userId);
            foreach (var transaction in list)
            {
                ObjectContext.DeleteObject(transaction);
            }
            list = ObjectContext.TransactionSet.Where(a => a.aspnet_Users.UserId == userId);
            foreach (var transaction in list)
            {
                ObjectContext.DeleteObject(transaction);
            }
            ObjectContext.SaveChanges();
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

        public void CreateTransaction(LoadMoneyViewModel model, PaymentMethod method)
        {
            using (new UnitOfWorkScope(true))
            {
                var transaction = new Transaction
                      {
                          Sum = model.Sum,
                          Comment = model.Comment,
                          Balance = model.Balance,
                          acc_PaymentsMethods = method,
                          Date = DateTime.Now,
                          astra_Clients =
                              ObjectContext.ClientSet.Where(c => c.UserId == model.ClientId).FirstOrDefault(),
                          aspnet_Users =
                              ObjectContext.ASPUserSet.Where(u => u.UserId == model.UserId).FirstOrDefault()
                      };
                ObjectContext.AddToTransactionSet(transaction);
                ObjectContext.SaveChanges(); 
            }

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