using System.Collections.Generic;
using System.Web.Mvc;
using ContactManager.Accounts.Interfaces;
using ContactManager.Models;
using ContactManager.Models.Validation;

namespace ContactManager.Accounts.Models
{
    public class PaymentMethodService : IPaymentMethodService
    {
        private readonly IPaymentMethodRepository _paymentMethodRepository;
        private IValidationDictionary _validationDictionary;

        public PaymentMethodService(IValidationDictionary validationDictionary)
        {
            _validationDictionary = validationDictionary;
            _paymentMethodRepository = new PaymentMethodRepository();
        }

        public List<PaymentMethod> ListPaymentMethods(bool? visible)
        {
            return _paymentMethodRepository.ListPaymentMethods(visible);
        }

        public bool Create(PaymentMethod paymentMethod)
        {
            try
            {
                _paymentMethodRepository.CreatePaymentMethod(paymentMethod);
                return true;
            }
            catch
            {
                return false;
            }

        }

        public PaymentMethod GetPaymentMethod(int id)
        {
            return _paymentMethodRepository.GetPaymentMethod(id);
        }

        public bool EditPaymentMethod(PaymentMethod paymentMethod)
        {
            try
            {
                _paymentMethodRepository.EditPaymentMethod(paymentMethod);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public SelectList SelectListPaymentMethods(int? selectedId)
        {
            var list = ListPaymentMethods(true);
            if (selectedId.HasValue)
                return new SelectList(list, "MethodId", "Name", selectedId.Value);

            var paymentMethod = new PaymentMethod { MethodId = 0, Name = "Please select", Visible = true };
            list.Add(paymentMethod);
            return new SelectList(list, "MethodId", "Name");
        }
    }
}
