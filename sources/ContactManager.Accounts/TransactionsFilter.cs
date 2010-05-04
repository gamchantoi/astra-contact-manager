using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using ContactManager.Accounts.ViewModels;

namespace ContactManager.Accounts
{
    public class TransactionsFilter
    {
        #region Properties
        public SelectList YearsList { get; set; }
        public SelectList MonthsList { get; set; }
        public DropDownViewModel PaymentMethodsList { get; set; }

        public int Years { get; set; }
        public int Months { get; set; }
        public string PaymentMethods { get; set; }
        private const string PAYMENT_METHODS_ID = "PaymentMethods";
        #endregion

        public TransactionsFilter()
        {
            InitPaymentMethodsList(null);
        }

        private void InitPaymentMethodsList(string paymentValues)
        {
            PaymentMethodsList = new DropDownViewModel
                                     {
                                         Name = PAYMENT_METHODS_ID,
                                         ItemsList = string.IsNullOrEmpty(paymentValues) 
                                                    ? new DropDownItemsList()
                                                    : new DropDownItemsList(paymentValues)
                                     };
        }

        public TransactionsFilter(FormCollection formCollection)
        {
            Years = int.Parse(formCollection.GetValue("Years").AttemptedValue);
            Months = int.Parse(formCollection.GetValue("Months").AttemptedValue);

            PaymentMethods = formCollection.GetValue(PAYMENT_METHODS_ID).AttemptedValue;

            InitPaymentMethodsList(formCollection.GetValue(PAYMENT_METHODS_ID).AttemptedValue);
        }

        public void AddPaymentMethods(PaymentMethod method, SelectList list)
        {
            if (PaymentMethodsList == null)
                InitPaymentMethodsList(null);
            
            PaymentMethodsList.ItemsList.Add(method, list);
        }

    }
}
