using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ContactManager.Models;

namespace ContactManager.Intefaces
{
    public interface IAccountTransactionService
    {
        List<AccountTransaction> ListTransaction();
        List<AccountTransaction> ListTransaction(Guid userId);
        bool CreateTransaction(Client client);
        bool DeleteTransactions(Guid userId);

        void ProcessClientPayment();
    }
}
