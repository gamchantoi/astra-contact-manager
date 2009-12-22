using System.Collections.Generic;
using System.Linq;
using ContactManager.Accounts.Interfaces;
using ContactManager.Models;

namespace ContactManager.Accounts.Models
{
    public class PaymentMethodRepository : RepositoryBase<PaymentMethod>, IPaymentMethodRepository
    {
        public List<PaymentMethod> ListPaymentMethods(bool? visible)
        {
            if(visible.HasValue)
                return ObjectContext.PaymentMethods.Where(m => m.Visible == visible.Value).ToList();
            return ObjectContext.PaymentMethods.ToList();
        }

        public void CreatePaymentMethod(PaymentMethod paymentMethod)
        {
            ObjectContext.AddToPaymentMethods(paymentMethod);
            ObjectContext.SaveChanges();
        }

        public PaymentMethod GetPaymentMethod(int id)
        {
            return ObjectContext.PaymentMethods.Where(m => m.MethodId == id).FirstOrDefault();
        }

        public PaymentMethod EditPaymentMethod(PaymentMethod paymentMethod)
        {
            var _paymentMethod = GetPaymentMethod(paymentMethod.MethodId);
            ObjectContext.ApplyPropertyChanges(_paymentMethod.EntityKey.EntitySetName, paymentMethod);
            ObjectContext.SaveChanges();
            return _paymentMethod;
        }

    }
}
