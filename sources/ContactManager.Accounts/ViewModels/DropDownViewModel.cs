using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContactManager.Accounts.ViewModels
{
    public class DropDownViewModel
    {
        public DropDownItemsList ItemsList { get; set; }
        public string Name { get; set; }

        public void SetAllSelected()
        {
            var list = new DropDownItemsList();
            
            foreach (var items in ItemsList)
            {
                var _items = new List<SelectListItem>();
                var _selected = new List<string>();

                foreach (var item in items.Value)
                {
                    _items.Add(
                        new SelectListItem
                        {
                            Value = item.Value,
                            Text = item.Text,
                            Selected = true
                        });
                    _selected.Add(item.Value);
                }
                list.Add(items.Key, new MultiSelectList(_items, "Value", "Text", _selected));
            }

            ItemsList = list;
        }
    }

    public class DropDownItemsList : Dictionary<string, MultiSelectList>
    {
        private const string PAYMENT_VALUES = "PAYMENT_VALUES";

        #region Constructors
        public DropDownItemsList()
        {
        }

        public DropDownItemsList(string paymentValues)
        {
            HttpContext.Current.Session.Add(PAYMENT_VALUES, paymentValues);
        } 
        #endregion

        public void Add(PaymentMethod method, SelectList list)
        {
            //todo: add items to colection and read from session ddefault values
            Add(method.ToString(), BuildList(list, method));

        }

        private MultiSelectList BuildList(SelectList list, PaymentMethod method)
        {
            var storedValues = GetSessionValues(method);
            var _list = new List<SelectListItem>();

            foreach (var item in list)
            {
                _list.Add(
                    new SelectListItem
                    {
                        Value = string.Format("{0}.{1}", method, item.Value),
                        Text = item.Text,
                        Selected = true
                    });
            }
            
            return new MultiSelectList(_list, "Value", "Text", storedValues);
        }

        private static List<string> GetSessionValues(PaymentMethod method)
        {
            var sessionValus = HttpContext.Current.Session[PAYMENT_VALUES];
            var storedValues = new List<string>();
            if (sessionValus != null)
            {
                foreach (var str in sessionValus.ToString().Split(','))
                {
                    var _str = str.Split('.');
                    if (_str[0].Equals(method.ToString()))
                        storedValues.Add(str);
                }
            }
            return storedValues;
        }

        public string GetValues(PaymentMethod method, string str)
        {
            var listVal = str.Split(',').Where(s => s.Contains(method.ToString())).ToList();
            var retVal = string.Empty;
            foreach (var item in listVal)
            {
                if (!string.IsNullOrEmpty(retVal))
                    retVal += ",";
                retVal += item.Split('.')[1];
            }

            return retVal;
        }
    }


    public enum PaymentMethod
    {
        Profile,
        Method,
        Service
    }
}