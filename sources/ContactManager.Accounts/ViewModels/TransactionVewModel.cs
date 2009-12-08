using System;

namespace ContactManager.Accounts.ViewModels
{
    public class TransactionVewModel
    {
        public string Sum { get; set; }
        public double Balance { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }
        
        public string ClientName { get; set; }
        public string UserName { get; set; }
        public string TransactionName { get; set; }
    }
}
