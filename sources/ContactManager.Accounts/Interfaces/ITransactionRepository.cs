using System;
using System.Collections.Generic;
using ContactManager.Models;

namespace ContactManager.Accounts.Interfaces
{
    public interface ITransactionRepository
    {
        List<Transaction> ListTransaction();
        List<Transaction> ListTransaction(Guid userId);
        bool CreateTransaction(Transaction transaction);
        bool DeleteTransactions(Guid userId);

        void ProcessClientPayment();
    }
}