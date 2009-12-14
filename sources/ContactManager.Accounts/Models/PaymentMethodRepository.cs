using System.Collections.Generic;
using System.Linq;
using ContactManager.Accounts.Interfaces;
using ContactManager.Models;

namespace ContactManager.Accounts.Models
{
    public class PaymentMethodRepository : IPaymentMethodRepository
    {
        #region Constructors
        public PaymentMethodRepository()
            : this(new AstraEntities())
        { }

        public PaymentMethodRepository(AstraEntities entities)
        {
            Entities = entities;
        } 
        #endregion

        public AstraEntities Entities { get; private set; }

        public List<PaymentMethod> ListPaymentMethods()
        {
            return Entities.PaymentMethodSet.ToList();
        }

        public void CreatePaymentMethod(PaymentMethod paymentMethod)
        {
            Entities.AddToPaymentMethodSet(paymentMethod);
            Entities.SaveChanges();
        }

        public PaymentMethod GetPaymentMethod(int id)
        {
            return Entities.PaymentMethodSet.Where(m => m.MethodId == id).FirstOrDefault();
        }

        public PaymentMethod EditPaymentMethod(PaymentMethod paymentMethod)
        {
            var _paymentMethod = GetPaymentMethod(paymentMethod.MethodId);
            Entities.ApplyPropertyChanges(_paymentMethod.EntityKey.EntitySetName, paymentMethod);
            Entities.SaveChanges();
            return _paymentMethod;
        }

    }
}
