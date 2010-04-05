using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using ContactManager.Accounts.Interfaces;
using ContactManager.Accounts.Models;
using ContactManager.Accounts.ViewModels;
using ContactManager.Models;
using ContactManager.Models.Validation;
using ContactManager.Models.ViewModels;
using ContactManager.PPP.Intefaces;
using ContactManager.PPP.Services;

namespace ContactManager.Accounts.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IValidationDictionary _validationDictionary;
        private readonly ITransactionRepository _repository;
        private readonly IProfileService _profileService;
        //private readonly IServiceService _serviceService;

        #region Constructors
        public TransactionService(IValidationDictionary validationDictionary)
        {
            _validationDictionary = validationDictionary;
            _repository = new EntityTransactionRepository();
            PaymentMethodService = new PaymentMethodService(validationDictionary);
            _profileService = new ProfileService(validationDictionary);
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
            var ctx = new CurrentContext();
            _repository.ProcessClientPayment(ctx.CurrentUserId);
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

        public SelectList GetTransactionYears()
        {
            var list = _repository.GetTransactionYears();
            return new SelectList(list, null);
        }

        public SelectList GetTransactionMonths()
        {
            var selectList = new List<KeyValuePair<string, string>>();

            foreach (var item in _repository.GetTransactionMonths())
            {
                selectList.Add(new KeyValuePair<string, string>(
                    item.ToString(),
                    CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(int.Parse(item.ToString()))
                    ));
            }
            return new SelectList(selectList, "key", "value", DateTime.Now.Month.ToString());
        }

        public Filter GetFilter()
        {
            var filter = new Filter
                             {
                                 YearsList = GetTransactionYears(),
                                 MonthsList = GetTransactionMonths(),
                                 PaymentMethodsList = FillPaymentMethodsList()
                             };
            return filter;
        }

        private SelectList FillPaymentMethodsList()
        {
            var methods = PaymentMethodService.SelectListPaymentMethods(0);
            var profiles = _profileService.SelectListProfiles(0);

            var selectList = new List<KeyValuePair<string, string>>();

            foreach (var item in methods)
            {
                selectList.Add(new KeyValuePair<string, string>(
                    "method." + item,
                    item.Text
                    ));
            }

            foreach (var item in profiles)
            {
                selectList.Add(new KeyValuePair<string, string>(
                    "profile." + item,
                    item.Text
                    ));
            }
            return new SelectList(selectList, "key", "value", DateTime.Now.Month.ToString());

            return PaymentMethodService.SelectListPaymentMethods(0);
        }

        public List<Transaction> ListTransactions(Filter filter)
        {

            return _repository.ListTransaction(filter);
        }

    }
}