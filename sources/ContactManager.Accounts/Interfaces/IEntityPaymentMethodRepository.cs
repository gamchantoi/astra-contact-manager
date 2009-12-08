using System.Collections;
using System.Collections.Generic;
using ContactManager.Models;

namespace ContactManager.Accounts.Interfaces
{
    interface IEntityPaymentMethodRepository
    {
        List<PaymentMethod> ListPaymentMethods();
        void Create(ContactManager.Models.PaymentMethod paymentMethod);
        PaymentMethod GetPaymentMethod(int id);
        PaymentMethod EditPaymentMethod(PaymentMethod paymentMethod);
    }
}
