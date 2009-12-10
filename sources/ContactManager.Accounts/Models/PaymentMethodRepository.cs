using System.Collections.Generic;
using System.Linq;
using ContactManager.Accounts.Interfaces;
using ContactManager.Models;

namespace ContactManager.Accounts.Models
{
    public class PaymentMethodRepository : IPaymentMethodRepository
    {
        private readonly AstraEntities _entities;

        public PaymentMethodRepository()
        {
            _entities = new AstraEntities();
        }

        #region ListPaymentMethods()

        public List<PaymentMethod> ListPaymentMethods()
        {
            return _entities.PaymentMethodSet.ToList();
        }

        #endregion

        #region Create(PaymentMethod paymentMethod)


        public void CreatePaymentMethod(PaymentMethod paymentMethod)
        {
            _entities.AddToPaymentMethodSet(paymentMethod);
            _entities.SaveChanges();
        }

        #endregion

        #region GetPaymentMethod(int id)


        public PaymentMethod GetPaymentMethod(int id)
        {
            PaymentMethod paymentMethod =
                (from m in _entities.PaymentMethodSet where m.MethodId == id select m).FirstOrDefault();
            return paymentMethod;
        }

        #endregion

        #region EditPaymentMethod(PaymentMethod paymentMethod)


        public PaymentMethod EditPaymentMethod(PaymentMethod paymentMethod)
        {
            var _paymentMethod = GetPaymentMethod(paymentMethod.MethodId);
            _entities.ApplyPropertyChanges(_paymentMethod.EntityKey.EntitySetName, paymentMethod);
            _entities.SaveChanges();
            return paymentMethod;
        }

        #endregion
    }
}
