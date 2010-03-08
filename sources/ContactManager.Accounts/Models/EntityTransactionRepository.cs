using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ContactManager.Accounts.Interfaces;
using ContactManager.Accounts.ViewModels;
using ContactManager.Models;
using ContactManager.Models.ViewModels;
using Microsoft.Data.Extensions;
using System.Data.SqlClient;

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

        public void ProcessClientPayment(Guid userId)
        {
            var param = new[] { new SqlParameter("@UserId", userId), };

            var command = ObjectContext.CreateStoreCommand("astra_ProcessPayments", CommandType.StoredProcedure, param);
            using (command.Connection.CreateConnectionScope())
                command.ExecuteScalar();

            command = ObjectContext.CreateStoreCommand("astra_Clients_Calculate_Statuses", CommandType.StoredProcedure);
            using (command.Connection.CreateConnectionScope())
                command.ExecuteScalar();
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