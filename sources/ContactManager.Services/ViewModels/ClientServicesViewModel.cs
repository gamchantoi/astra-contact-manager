using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContactManager.Models;

namespace ContactManager.Services.ViewModels
{
    public class ClientServicesViewModel
    {
        public List<Service> ListServices { get; set; }
        public List<Service> UserServices { get; set; }
    }
}
