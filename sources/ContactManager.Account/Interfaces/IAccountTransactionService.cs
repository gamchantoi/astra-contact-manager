using System;
using System.Collections.Generic;
using ContactManager.Models;

namespace ContactManager.Account.Interfaces
{
    public interface IAccountTransactionService
    {
        List<AccountTransaction> ListTransactions();
        List<AccountTransaction> ListTransactions(Guid userId);
        bool CreateTransaction(Client client);
        bool CreateTransaction(Client client, Service service, Guid userId);
        bool DeleteTransactions(Guid userId);

        void ProcessClientPayment();
    }
}