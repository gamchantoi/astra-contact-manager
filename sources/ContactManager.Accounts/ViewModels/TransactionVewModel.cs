﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ContactManager.Accounts.ViewModels
{
    public class TransactionViewModel
    {
        public IList<Transactions> Transactions { get; set; }
        public TransactionsFilter Filter { get; set; }
        public decimal TotalSum { get; set; }
    }

    public class Transactions
    {
        public decimal Sum { get; set; }
        public decimal Balance { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }

        public string ClientUserName { get; set; }
        public Guid ClientUserId { get; set; }
        public string UserUserName { get; set; }
        public string TransactionName { get; set; }

        public SelectList Filter_Years { get; set; }
        public SelectList Filter_Month { get; set; }
    }
}
