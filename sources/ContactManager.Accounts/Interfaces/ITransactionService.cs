using System;
using System.Collections.Generic;
using ContactManager.Models;

namespace ContactManager.Accounts.Interfaces
{
    public interface ITransactionService
    {
        List<Transaction> ListTransactions();
        List<Transaction> ListTransactions(Guid userId);
        bool CreateTransaction(Client client);
        bool CreateTransaction(Client client, Service service, Guid userId);
        bool DeleteTransactions(Guid userId);

        void ProcessClientPayment();
    }
}