using System;
using System.Collections.Generic;
using ContactManager.Accounts.Helpers;
using ContactManager.Accounts.Interfaces;
using ContactManager.Accounts.Models;
using ContactManager.Models;
using ContactManager.Models.Validation;
using ContactManager.Models.ViewModels;

namespace ContactManager.Accounts.Services
{
    public class TransactionService : ITransactionService
    {
        private IValidationDictionary _validationDictionary;
        private readonly ITransactionRepository _repository;

        #region Constructors
        public TransactionService(IValidationDictionary validationDictionary)
            : this(validationDictionary, new AstraEntities())
        { }

        public TransactionService(IValidationDictionary validationDictionary, AstraEntities entirties)
        {
            _validationDictionary = validationDictionary;
            _repository = new EntityTransactionRepository(entirties);
            PaymentMethodService = new PaymentMethodService();        }
        #endregion

        #region IAccountTransactionService Members

        public List<Transaction> ListTransactions()
        {
            return _repository.ListTransaction();
        }

        public List<Transaction> ListTransactions(Guid userId)
        {
            return _repository.ListTransaction(userId);
        }

        public bool CreateTransaction(LoadMoneyViewModel model)
        {
            throw new System.NotImplementedException();
        }

        public PaymentMethodService PaymentMethodService { get; private set; }

        public void ProcessClientPayment()
        {
            _repository.ProcessClientPayment();
        }

        #endregion
    }
}