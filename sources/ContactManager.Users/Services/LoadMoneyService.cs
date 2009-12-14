using System;
using ContactManager.Accounts.Interfaces;
using ContactManager.Accounts.Services;
using ContactManager.Models;
using ContactManager.Models.Validation;
using ContactManager.Models.ViewModels;
using ContactManager.Users.Interfaces;

namespace ContactManager.Users.Services
{
    public class LoadMoneyService : ILoadMoneyService
    {
        private readonly IValidationDictionary _validationDictionary;
        private readonly IClientService _clientService;
        private readonly ITransactionService _transactionService;

        #region Constructors
        public LoadMoneyService(IValidationDictionary validationDictionary)
            : this(validationDictionary, new AstraEntities())
        {
        }

        public LoadMoneyService(IValidationDictionary validationDictionary, AstraEntities entities)
        {
            _validationDictionary = validationDictionary;
            _clientService = new ClientService(validationDictionary, entities);
            _transactionService = new TransactionService(validationDictionary, entities);
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
                if (model.MethodId == 0)
                {
                    _validationDictionary.AddError("MethodId", "Please select Peyment Method");
                    return false;
                }

                var client = _clientService.GetClient(model.UserId);
                model.Balance = client.Balance;

                //var transaction = _transactionService.GetTransaction(model);
                //transaction.Client = client;

                var transaction = new Transaction
                  {
                      Sum = model.Sum,
                      Comment = model.Comment,
                      Balance = model.Balance,
                      acc_PaymentsMethods =
                          _transactionService.PaymentMethodService.GetPaymentMethod(model.MethodId),
                      //astra_Clients = client,
                      Client = client
                  };


                //transaction.aspnet_Users = _userFasade.MembershipService.;
                _transactionService.CreateTransaction(transaction);
                
                client.Balance = client.Balance + model.Sum;
                _clientService.EditClient(client);
                return true;
            }
            catch (Exception ex)
            {
                _validationDictionary.AddError("_FORM", "Money is not loaded. " + ex.Message);
                return false;
            }

        }
    }
}
