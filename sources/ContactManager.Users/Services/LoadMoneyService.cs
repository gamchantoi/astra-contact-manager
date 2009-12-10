using System;
using ContactManager.Accounts.Interfaces;
using ContactManager.Accounts.Services;
using ContactManager.Models.Validation;
using ContactManager.Models.ViewModels;
using ContactManager.Users.Interfaces;
using ContactManager.Users.Models;

namespace ContactManager.Users.Services
{
    public class LoadMoneyService : ILoadMoneyService
    {
        private readonly IValidationDictionary _validationDictionary;
        private readonly IClientService _clientService;
        private readonly ITransactionService _transactionService;

        #region Constructors
        public LoadMoneyService(IValidationDictionary validationDictionary)
            : this(validationDictionary, new ClientService(validationDictionary))
        {
        }

        public LoadMoneyService(IValidationDictionary validationDictionary,
            IClientService clientService)
        {
            _validationDictionary = validationDictionary;
            _clientService = clientService;
            _transactionService = new TransactionService(validationDictionary);
        }
        #endregion

        public LoadMoneyViewModel GetViewModel(Guid UserId)
        {
            var client = _clientService.GetClient(UserId);
            client.LoadDetailsReferences();
            var model = new LoadMoneyViewModel
                            {
                                UserId = client.UserId,
                                UserName = client.UserName,
                                Balance = client.Balance,
                                FullName = string.Format("{0} ({1})", client.FullName, client.UserName),
                                LoadMethods = _transactionService.PaymentMethodService.ListPaymentMethods(null)
                            };
            return model;
        }

        public bool LoadMoney(LoadMoneyViewModel model)
        {
            try
            {
                if (model.MethodId != 0)
                {
                    var client = _clientService.GetClient(model.UserId);
                    model.Balance = client.Balance;
                    client.Balance = client.Balance + model.Sum;
                    _clientService.EditClient(client);
                    var transaction = _transactionService.GetTransaction(model);
                    
                    transaction.Client = client;
                   _transactionService.CreateTransaction(transaction);
                    return true;
                }
                _validationDictionary.AddError("MethodId", "Please select Method");
                return false;
            }
            catch (Exception ex)
            {
                _validationDictionary.AddError("_FORM", "Money not loaded. " + ex.Message);
                return false;
            }
            
        }
    }
}
