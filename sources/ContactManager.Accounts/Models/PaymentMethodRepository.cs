using System.Collections.Generic;
using System.Linq;
using ContactManager.Accounts.Interfaces;
using ContactManager.Models;

namespace ContactManager.Accounts.Models
{
    public class PaymentMethodRepository : RepositoryBase<PaymentMethod>, IPaymentMethodRepository
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
            return ObjectContext.PaymentMethodSet.ToList();
        }

        public void CreatePaymentMethod(PaymentMethod paymentMethod)
        {
            ObjectContext.AddToPaymentMethodSet(paymentMethod);
            ObjectContext.SaveChanges();
        }

        public PaymentMethod GetPaymentMethod(int id)
        {
            return ObjectContext.PaymentMethodSet.Where(m => m.MethodId == id).FirstOrDefault();
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
