namespace ContactManager.Models
{
    partial class Transaction
    {
        public Client Client { get; set; }
        public ASPUser User { get; set; }

        public string GetTransactionName()
        {
            if (EntityKey == null) return string.Empty;

            var retVal = string.Empty;
            astra_ServicesReference.Load();
            mkt_PPPProfilesReference.Load();
            acc_PaymentsMethodsReference.Load();

            if (astra_ServicesReference.Value != null)
                retVal = "Service: " + astra_ServicesReference.Value.Name;
            if (mkt_PPPProfilesReference.Value != null)
                retVal = "Profile: " + mkt_PPPProfilesReference.Value.Name;
            if (acc_PaymentsMethodsReference.Value != null)
                retVal = "Method: " + acc_PaymentsMethodsReference.Value.Name;

            return retVal;
        }
    }
}
