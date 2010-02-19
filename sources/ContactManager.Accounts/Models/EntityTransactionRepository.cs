using System;
using System.Collections.Generic;
using System.Linq;
using ContactManager.Accounts.Interfaces;
using ContactManager.Accounts.ViewModels;
using ContactManager.Models;
using ContactManager.Models.ViewModels;

namespace ContactManager.Accounts.Models
{
    public class EntityTransactionRepository : RepositoryBase<Transaction>, ITransactionRepository
    {
        public Transaction GetTransaction(LoadMoneyViewModel model)
        {
            return new Transaction
            {
                Sum = model.Sum,
                Comment = model.Comment,
                Balance = model.Balance
            };
        }

        public List<Transaction> ListTransaction()
        {
            var list = ObjectContext.Transactions.ToList();
            foreach (var item in list)
            {
                item.ClientReference.Load();
                item.Client = item.ClientReference.Value;
                item.UserReference.Load();
                item.User = item.UserReference.Value;
            }
            return list;
        }

        public List<Transaction> ListTransaction(Guid userId)
        {
            var user = ObjectContext.Clients.Where(c => c.UserId == userId).FirstOrDefault();
            user.acc_Transactions.Load();
            var list = user.acc_Transactions.ToList();
            return list;
        }

        public bool CreateTransaction(Transaction transaction)
        {
            transaction.Date = DateTime.Now;
            ObjectContext.AddToTransactions(transaction);
            ObjectContext.SaveChanges();
            return true;
        }

        public bool DeleteTransactions(Guid userId)
        {
            var list = ObjectContext.Transactions.Where(a => a.Client.UserId == userId);
            foreach (var transaction in list)
            {
                ObjectContext.DeleteObject(transaction);
            }
            list = ObjectContext.Transactions.Where(a => a.User.UserId == userId);
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

        public IQueryable<int> GetTransactionYears()
        {
            return ObjectContext.Transactions.Select(t => t.Date.Year).Distinct();
        }

        public IQueryable<int> GetTransactionMonths()
        {
            return ObjectContext.Transactions.Select(t => t.Date.Month).Distinct();
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


        public List<Transaction> ListTransaction(Filter filter)
        {
            //var filterYear = filter.Years.SelectedValue

            var list = ObjectContext.Transactions.Where(y => y.Date.Year == filter.Years).ToList();
            list = list.Where(m => m.Date.Month == filter.Months).ToList();
            foreach (var item in list)
            {
                item.ClientReference.Load();
                item.Client = item.ClientReference.Value;
                item.UserReference.Load();
                item.User = item.UserReference.Value;
            }
            return list;
        }
    }
}