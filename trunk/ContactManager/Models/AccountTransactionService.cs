using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContactManager.Intefaces;
using ContactManager.Models.Validation;

namespace ContactManager.Models
{
    public class AccountTransactionService : IAccountTransactionService
    {
        private IValidationDictionary _validationDictionary;
        private readonly IAccountTransactionRepository _repository;
        private readonly IMembershipService _accountService;

        #region Constructors
        public AccountTransactionService(IValidationDictionary validationDictionary)
            : this(validationDictionary, new EntityAccountTransactionRepository())
        { }

        public AccountTransactionService(IValidationDictionary validationDictionary, IAccountTransactionRepository repository)
        {
            _validationDictionary = validationDictionary;
            _repository = repository;
            _accountService = new AccountMembershipService();
        }
        #endregion

        #region IAccountTransactionService Members

        public List<AccountTransaction> ListTransaction()
        {
            return _repository.ListTransaction();
        }

        public bool CreateTransaction(Client client)
        {
            if (client.Load == 0)
                return false;
            var transaction = new AccountTransaction
            {
                ClientId = client.UserId,
                UserId = new Guid(_accountService.GetCurrentUser().ProviderUserKey.ToString()),
                MethodId = client.MethodId,
                Sum = client.Load,
                Balance = client.Balance,
                Date = DateTime.Now,
                Comment = client.Comment
            };
            _repository.CreateTransaction(transaction);
            return true;
        }

        public bool DeleteTransactions(Guid userId)
        {
            return _repository.DeleteTransactions(userId);
        }

        public List<AccountTransaction> ListTransaction(Guid userId)
        {
            return _repository.ListTransaction(userId);
        }

        public void ProcessClientPayment()
        {
            _repository.ProcessClientPayment();
        }

        #endregion
    }
}
