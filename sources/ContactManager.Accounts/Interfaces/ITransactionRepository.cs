using System;
using System.Collections.Generic;
using System.Linq;
using ContactManager.Accounts.ViewModels;
using ContactManager.Models;

namespace ContactManager.Accounts.Interfaces
{
    public interface ITransactionRepository
    {
        List<Transaction> ListTransaction();
        List<Transaction> ListTransaction(Guid userId);
        bool CreateTransaction(Transaction transaction);
        bool DeleteTransactions(Guid userId);
        IQueryable<int> GetTransactionYears();
        void ProcessClientPayment(Guid userId);

        List<Transaction> ListTransaction(Filter filter);

        IQueryable<int> GetTransactionMonths();
    }
}