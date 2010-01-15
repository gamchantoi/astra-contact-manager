using System;
using System.Web.Mvc;

namespace ContactManager.Accounts.ViewModels
{
    public class TransactionVewModel
    {
        public double Sum { get; set; }
        public double Balance { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }

        public string ClientUserName { get; set; }
        public string UserUserName { get; set; }
        public string TransactionName { get; set; }

        public SelectList Filter_Years { get; set; }
        public SelectList Filter_Month { get; set; }
    }
}
