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

        // todo: check this code 
        public List<Transaction> ListTransaction1(Filter filter)
        {

            var list = ObjectContext.Transactions.Where(y => y.Date.Year == filter.Years).ToList();
            list = list.Where(m => m.Date.Month == filter.Months).ToList();

            var filtredlist = new List<Transaction>();
            foreach (var item in list)
            {
                item.ClientReference.Load();
                item.Client = item.ClientReference.Value;
                item.UserReference.Load();
                item.User = item.UserReference.Value;
                item.PaymentMethodReference.Load();
                item.PaymentMethod = item.PaymentMethodReference.Value;
                if (item.PaymentMethod != null)
                    if (item.PaymentMethod.MethodId.ToString() == filter.PaymentMethods)
                        filtredlist.Add(item);
            }
            return filtredlist;
        }

        public List<Transactions> ListTransaction(Filter filter)
        {
            var users = new List<Transactions>();
            var startDate = new DateTime(filter.Years, filter.Months, 1);
            var endDate = startDate.AddMonths(1);

            var command = ObjectContext.CreateStoreCommand("astra_Transactions_List", CommandType.StoredProcedure);
            command.Parameters.Add(new SqlParameter { ParameterName = "@StartDate", Value = startDate, DbType = DbType.DateTime });
            command.Parameters.Add(new SqlParameter { ParameterName = "@EndDate", Value = endDate, DbType = DbType.DateTime });

            if (string.IsNullOrEmpty(GetValuesList(filter.PaymentMethods, "profile")))
                command.Parameters.Add(new SqlParameter { ParameterName = "@ProfilesIds", Value = DBNull.Value });
            else
                command.Parameters.Add(new SqlParameter { ParameterName = "@ProfilesIds", Value = GetValuesList(filter.PaymentMethods, "profile") });

            if (string.IsNullOrEmpty(GetValuesList(filter.PaymentMethods, "method")))
                command.Parameters.Add(new SqlParameter { ParameterName = "@MethodsIds", Value = DBNull.Value });
            else
                command.Parameters.Add(new SqlParameter { ParameterName = "@MethodsIds", Value = GetValuesList(filter.PaymentMethods, "method") });

            if (string.IsNullOrEmpty(GetValuesList(filter.PaymentMethods, "service")))
                command.Parameters.Add(new SqlParameter { ParameterName = "@ServicesIds", Value = DBNull.Value });
            else
                command.Parameters.Add(new SqlParameter { ParameterName = "@ServicesIds", Value = GetValuesList(filter.PaymentMethods, "service") });

            using (command.Connection.CreateConnectionScope())
            using (var dataReader = command.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    var model = new Transactions
                    {
                        //TransactionId = !dataReader.IsDBNull(4) ? dataReader.GetInt32(4) : 0,
                        ClientUserId = !dataReader.IsDBNull(1) ? dataReader.GetGuid(1) : Guid.Empty,
                        ClientUserName = !dataReader.IsDBNull(2) ? dataReader.GetString(2) : "",
                        UserUserName = !dataReader.IsDBNull(4) ? dataReader.GetString(4) : "",
                        Sum = !dataReader.IsDBNull(5) ? dataReader.GetDecimal(5) : decimal.Zero,
                        Balance = !dataReader.IsDBNull(6) ? dataReader.GetDecimal(6) : decimal.Zero,
                        Date = !dataReader.IsDBNull(7) ? dataReader.GetDateTime(7) : DateTime.MinValue,
                    };

                    //model.FullName = string.Format("{0} {1} {2}",
                    //    !dataReader.IsDBNull(6) ? dataReader.GetString(6) : "",
                    //    !dataReader.IsDBNull(7) ? dataReader.GetString(7) : "",
                    //    !dataReader.IsDBNull(8) ? dataReader.GetString(8) : "")
                    //    .Trim();
                    //if (model.UserName != "System")
                    users.Add(model);
                }
            }

            return users;
        }

        private static string GetValuesList(string str, string key)
        {
            var listVal = str.Split(',').Where(s => s.Contains(key));
            var retVal = String.Empty;
            foreach (var item in listVal)
            {
                if (!string.IsNullOrEmpty(retVal))
                    retVal += ",";
                retVal += item.Split('.')[1];
            }

            return retVal;
        }

    }
}