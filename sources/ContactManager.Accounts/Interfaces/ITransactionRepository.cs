using System;
using System.Collections.Generic;
using ContactManager.Models;
using ContactManager.Models.ViewModels;

namespace ContactManager.Accounts.Interfaces
{
    public interface ITransactionRepository
    {
        List<Transaction> ListTransaction();
        List<Transaction> ListTransaction(Guid userId);
        bool CreateTransaction(Transaction transaction);
        bool DeleteTransactions(Guid userId);
        Transaction GetTransaction(LoadMoneyViewModel model);

        AstraEntities Entities { get; }

        void ProcessClientPayment();
        void CreateTransaction(LoadMoneyViewModel model, PaymentMethod method);

        void Add(Transaction transaction);
    }
}