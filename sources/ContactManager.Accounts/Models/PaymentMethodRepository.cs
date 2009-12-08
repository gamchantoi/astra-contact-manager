using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContactManager.Accounts.Interfaces;
using ContactManager.Models;

namespace ContactManager.Accounts.Models
{
    public class PaymentMethodRepository : IEntityPaymentMethodRepository
    {
        private AstraEntities _entities = new AstraEntities();

        #region ListPaymentMethods()

        public List<PaymentMethod> ListPaymentMethods()
        {
            var paymentMethod = _entities.PaymentMethodSet.ToList();
            return paymentMethod;
        }

        #endregion

        #region Create(PaymentMethod paymentMethod)


        public void Create(PaymentMethod paymentMethod)
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
