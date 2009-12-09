using System.Collections.Generic;
using ContactManager.Models;

namespace ContactManager.Accounts.Interfaces
{
    interface IPaymentMethodRepository
    {
        List<PaymentMethod> ListPaymentMethods();
        void CreatePaymentMethod(PaymentMethod paymentMethod);
        PaymentMethod GetPaymentMethod(int id);
        PaymentMethod EditPaymentMethod(PaymentMethod paymentMethod);
    }
}
