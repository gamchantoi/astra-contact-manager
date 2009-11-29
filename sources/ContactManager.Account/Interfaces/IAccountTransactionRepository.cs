using System;
using System.Collections.Generic;
using ContactManager.Models;

namespace ContactManager.Account.Interfaces
{
    public interface IAccountTransactionRepository
    {
        List<AccountTransaction> ListTransaction();
        List<AccountTransaction> ListTransaction(Guid userId);
        bool CreateTransaction(AccountTransaction transaction);
        bool DeleteTransactions(Guid userId);

        void ProcessClientPayment();
    }
}