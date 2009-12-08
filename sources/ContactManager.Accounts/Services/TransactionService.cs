using System;
using System.Collections.Generic;
using ContactManager.Accounts.Helpers;
using ContactManager.Accounts.Interfaces;
using ContactManager.Accounts.Models;
using ContactManager.Models;
using ContactManager.Models.Validation;

namespace ContactManager.Accounts.Services
{
    public class TransactionService : ITransactionService
    {
        private IValidationDictionary _validationDictionary;
        private readonly ITransactionRepository _repository;
        //private readonly IMembershipService _accountService;
        //private readonly IAccountTransactionMethodService _methodService;
        private readonly UserHelper _helper;

        #region Constructors
        public TransactionService(IValidationDictionary validationDictionary)
            : this(validationDictionary, new AstraEntities())
        { }

        public TransactionService(IValidationDictionary validationDictionary, AstraEntities entirties)
        {
            _validationDictionary = validationDictionary;
            _repository = new EntityTransactionRepository(entirties);
            //_accountService = new AccountMembershipService();
            //_methodService = new AccountTransactionMethodService(validationDictionary);
            _helper = new UserHelper();
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

        public bool CreateTransaction(Client client)
        {
            if (client.Load == 0)
                return false;

            var userId = _helper.CurrentUserId;
            return CreateTransaction(client, client.MethodId, null, null, client.Load, userId, "");
        }   

        public bool CreateTransaction(Client client, Service service, Guid userId)
        {
            return false;
            //return CreateTransaction(client, _methodService.GetServiceMethod().MethodId, service.ServiceId, null, - service.Cost, userId, "");
        }

        private bool CreateTransaction(Client client, int methodId, int? serviceId, int? profileId, decimal sum, Guid userId, string comment)
        {
            //todo: implement ProfileID if needed

            var transaction = new Transaction
                                  {
                                      Sum = sum,
                                      Balance = client.Balance,
                                      Date = DateTime.Now,
                                      Comment = comment
                                  };
            _repository.CreateTransaction(transaction);

            return true;
        }

        public bool DeleteTransactions(Guid userId)
        {
            return _repository.DeleteTransactions(userId);
        }

        public void ProcessClientPayment()
        {
            _repository.ProcessClientPayment();
        }

        #endregion
    }
}