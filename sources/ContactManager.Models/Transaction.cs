namespace ContactManager.Models
{
    partial class Transaction
    {
        public string GetTransactionName()
        {
            if (EntityKey == null) return string.Empty;

            var retVal = string.Empty;
            ServiceReference.Load();
            ProfileReference.Load();
            PaymentMethodReference.Load();

            if (ServiceReference.Value != null)
                retVal = "Service: " + ServiceReference.Value.Name;
            if (ProfileReference.Value != null)
                retVal = "Profile: " + ProfileReference.Value.Name;
            if (PaymentMethodReference.Value != null)
                retVal = "Method: " + PaymentMethodReference.Value.Name;

            return retVal;
        }
    }
}
