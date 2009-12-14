using System;
using System.Collections.Generic;
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

        public TransactionService(IValidationDictionary validationDictionary, 
            AstraEntities entities)
        {
            _validationDictionary = validationDictionary;
            _repository = new EntityTransactionRepository(entities);
            PaymentMethodService = new PaymentMethodService(validationDictionary, entities);
        }
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

        public Transaction GetTransaction(LoadMoneyViewModel model)
        {
            var _model = _repository.GetTransaction(model);
            _model.acc_PaymentsMethods = PaymentMethodService.GetPaymentMethod(model.MethodId);
            return _model;
        }
        
        public PaymentMethodService PaymentMethodService { get; private set; }

        public void ProcessClientPayment()
        {
            _repository.ProcessClientPayment();
        }

        public void CreateTransaction(Transaction transaction)
        {
            _repository.CreateTransaction(transaction);
        }

        #endregion
    }
}