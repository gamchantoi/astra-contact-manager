using System;
using ContactManager.Accounts.Interfaces;
using ContactManager.Accounts.Services;
using ContactManager.Models.Enums;
using ContactManager.Models.Validation;
using ContactManager.Models.ViewModels;
using ContactManager.Users.Interfaces;

namespace ContactManager.Users.Services
{
    public class LoadMoneyService : ILoadMoneyService
    {
        private readonly IValidationDictionary _validationDictionary;
        private readonly IUserFasade _userFacade;
        private readonly ITransactionService _transactionService;

        #region Constructors
        public LoadMoneyService(IValidationDictionary validationDictionary)
            :this(validationDictionary, new UserFasade(validationDictionary))
        {
        }

        public LoadMoneyService(IValidationDictionary validationDictionary, IUserFasade facade)
        {
            _validationDictionary = validationDictionary;
            _userFacade = facade;
            _transactionService = new TransactionService(validationDictionary);
        }
        #endregion

        public LoadMoneyViewModel GetViewModel(Guid UserId)
        {
            var client = _userFacade.ClientService.GetClient(UserId);
            client.LoadDetailsReferences();
            var model = new LoadMoneyViewModel
                            {
                                ClientId = client.UserId,
                                UserName = client.UserName,
                                Balance = client.Balance,
                                FullName = string.Format("{0} ({1})", client.GetFullName(), client.UserName),
                                LoadMethods = _transactionService.PaymentMethodService.SelectListPaymentMethods(null)
                            };
            return model;
        }

        public bool LoadMoney(LoadMoneyViewModel model)
        {
            try
            {
                model.NeedUpdate = false;

                if (model.MethodId == 0)
                {
                    _validationDictionary.AddError("MethodId", "Please select Peyment Method");
                    return false;
                }

                var client = _userFacade.ClientService.GetClient(model.ClientId);
                

                model.Balance = client.Balance;

                _transactionService.CreateTransaction(model);
                
                client.Balance = client.Balance + model.Sum;

                client.LoadStatusReferences();
                if (!client.Status.IsActive && client.Balance > 0)
                {
                    client.Status = _userFacade.ClientService.StatusService.GetStatus(STATUSES.Active);

                    var secret = _userFacade.SecretService.GetPPPSecret(model.ClientId);
                    secret.Disabled = !client.Status.IsActive;
                    _userFacade.SecretService.EditPPPSecret(secret);
                    model.NeedUpdate = true;
                }
                _userFacade.ClientService.EditClient(client);

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
