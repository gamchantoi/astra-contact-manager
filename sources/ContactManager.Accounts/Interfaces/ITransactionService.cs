using System;
using System.Collections.Generic;
using ContactManager.Accounts.Models;
using ContactManager.Models;
using ContactManager.Models.ViewModels;

namespace ContactManager.Accounts.Interfaces
{
    public interface ITransactionService
    {
        List<Transaction> ListTransactions();
        List<Transaction> ListTransactions(Guid userId);
        bool CreateTransaction(LoadMoneyViewModel model);


        PaymentMethodService PaymentMethodService { get; }

        void ProcessClientPayment();
    }
}