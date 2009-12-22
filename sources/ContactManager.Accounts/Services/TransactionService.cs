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
        private readonly IValidationDictionary _validationDictionary;
        private readonly ITransactionRepository _repository;

        #region Constructors
        public TransactionService(IValidationDictionary validationDictionary)
        {
            _validationDictionary = validationDictionary;
            _repository = new EntityTransactionRepository();
            PaymentMethodService = new PaymentMethodService(validationDictionary);
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

        public PaymentMethodService PaymentMethodService { get; private set; }

        public void ProcessClientPayment()
        {
            _repository.ProcessClientPayment();
        }

        public bool CreateTransaction(LoadMoneyViewModel model)
        {
            try
            {
                var _ctx = new CurrentContext();
                var _transaction = new Transaction
                  {
                      Sum = model.Sum,
                      Comment = model.Comment,
                      Balance = model.Balance,
                      PaymentMethod = PaymentMethodService.GetPaymentMethod(model.MethodId),
                      User = _ctx.CurrentASPUser,
                      Client = _ctx.GetClient(model.ClientId)
                  };
                _repository.CreateTransaction(_transaction);
                return true;
            }
            catch (Exception ex)
            {
                _validationDictionary.AddError("_FORM", "Transaction is not saved. " + ex.Message);
                return false;
            }
        }

        #endregion
    }
}