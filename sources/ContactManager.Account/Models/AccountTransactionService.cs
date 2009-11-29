using System;
using System.Collections.Generic;
using ContactManager.Account.Helpers;
using ContactManager.Account.Interfaces;
using ContactManager.Account.Models;
using ContactManager.Models;
using ContactManager.Models.Validation;

namespace ContactManager.Account.Models
{
    public class AccountTransactionService : IAccountTransactionService
    {
        private IValidationDictionary _validationDictionary;
        private readonly IAccountTransactionRepository _repository;
        //private readonly IMembershipService _accountService;
        //private readonly IAccountTransactionMethodService _methodService;
        private readonly UserHelper _helper;

        #region Constructors
        public AccountTransactionService(IValidationDictionary validationDictionary)
            : this(validationDictionary, new EntityAccountTransactionRepository())
        { }

        public AccountTransactionService(IValidationDictionary validationDictionary, IAccountTransactionRepository repository)
        {
            _validationDictionary = validationDictionary;
            _repository = repository;
            //_accountService = new AccountMembershipService();
            //_methodService = new AccountTransactionMethodService(validationDictionary);
            _helper = new UserHelper();
        }
        #endregion

        #region IAccountTransactionService Members

        public List<AccountTransaction> ListTransactions()
        {
            return _repository.ListTransaction();
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

            var transaction = new AccountTransaction
                                  {
                                      ClientId = client.UserId,
                                      UserId = userId,
                                      MethodId = methodId,
                                      ServiceId = serviceId,
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

        public List<AccountTransaction> ListTransactions(Guid userId)
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