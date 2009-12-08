using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.UI.MobileControls;
using ContactManager.Accounts.Interfaces;
using ContactManager.Models;

namespace ContactManager.Accounts.Models
{
    public class PaymentMethodService : IPaymentMethodService
    {
        private IEntityPaymentMethodRepository _paymentMethodRepository = new PaymentMethodRepository();

        #region ListPaymentMethods()

        public List<PaymentMethod> ListPaymentMethods()
        {
            return _paymentMethodRepository.ListPaymentMethods();
        }

        #endregion

        #region Create(PaymentMethod paymentMethod)


        public bool Create(PaymentMethod paymentMethod)
        {
            try
            {
                _paymentMethodRepository.Create(paymentMethod);
                return true;
            }
            catch
            {
                return false;
            }

        }

        #endregion

        #region PaymentMethod GetPaymentMethod(int id)


        public PaymentMethod GetPaymentMethod(int id)
        {
            return _paymentMethodRepository.GetPaymentMethod(id);
        }

        #endregion

        #region EditPaymentMethod(PaymentMethod paymentMethod)


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

        #endregion

        public SelectList ListPaymentMethods(int? selectedId)
        {
            var list = ListPaymentMethods();
            var paymentMethod = new PaymentMethod { MethodId = 0, Name = "Please select", Visible = true };
            list.Add(paymentMethod);
            if (selectedId.HasValue)
                return new SelectList(list, "MethodId", "Name", selectedId.Value);
            return new SelectList(list, "MethodId", "Name");
        }
    }
}
