using System.Collections.Generic;
using System.Web.Mvc;
using ContactManager.Accounts.Interfaces;
using ContactManager.Models;

namespace ContactManager.Accounts.Models
{
    public class PaymentMethodService : IPaymentMethodService
    {
        private readonly IPaymentMethodRepository _paymentMethodRepository = new PaymentMethodRepository();

        public List<PaymentMethod> ListPaymentMethods()
        {
            return _paymentMethodRepository.ListPaymentMethods();
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

        public SelectList ListPaymentMethods(int? selectedId)
        {
            var list = ListPaymentMethods();
            if (selectedId.HasValue)
                return new SelectList(list, "MethodId", "Name", selectedId.Value);

            var paymentMethod = new PaymentMethod { MethodId = 0, Name = "Please select", Visible = true };
            list.Add(paymentMethod);
            return new SelectList(list, "MethodId", "Name");
        }
    }
}
