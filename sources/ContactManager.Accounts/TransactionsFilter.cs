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
            Years = int.Parse(GetValue(formCollection.GetValue("Years")));
            Months = int.Parse(GetValue(formCollection.GetValue("Months")));

            PaymentMethods = GetValue(formCollection.GetValue(PAYMENT_METHODS_ID));

            InitPaymentMethodsList(GetValue(formCollection.GetValue(PAYMENT_METHODS_ID)));
        }

        private static string GetValue(ValueProviderResult result)
        {
            if (result != null)
                return result.AttemptedValue;
            return string.Empty;
        }

        public void AddPaymentMethods(PaymentMethod method, SelectList list)
        {
            if (PaymentMethodsList == null)
                InitPaymentMethodsList(null);
            
            PaymentMethodsList.ItemsList.Add(method, list);
        }

        public string GetPymentMethodsValues(PaymentMethod method)
        {
            return PaymentMethodsList.ItemsList.GetValues(method, PaymentMethods);
        }

    }
}
