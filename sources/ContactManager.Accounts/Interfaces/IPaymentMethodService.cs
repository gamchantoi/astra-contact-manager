﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using ContactManager.Models;

namespace ContactManager.Accounts.Interfaces
{
    interface IPaymentMethodService
    {
        List<PaymentMethod> ListPaymentMethods();
        bool Create(PaymentMethod paymentMethod);
        PaymentMethod GetPaymentMethod(int id);
        SelectList ListPaymentMethods(int? selectedId);
        bool EditPaymentMethod(PaymentMethod paymentMethod);
    }
}
