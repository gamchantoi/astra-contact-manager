﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ContactManager.Accounts.ViewModels
{
    public class TransactionViewModel
    {
        public IList<Transactions> Transactions { get; set; }
        public Filter Filter { get; set; }
    }

    public class Transactions
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
    public class Filter
    {
        public SelectList YearsList { get; set; }
        public SelectList MonthsList  { get; set; }
        public SelectList PaymentMethodsList { get; set; }

        public int Years { get; set; }
        public int Months { get; set; }
        public int PaymentMethods { get; set; }
    }
}
