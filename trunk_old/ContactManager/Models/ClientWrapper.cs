using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContactManager.Intefaces;

namespace ContactManager.Models
{
    public class ClientWrapper : Client, IClientWrapper
    {
        #region IClientWrapper Members

        public string UserName { get; set; }
        public string Role { get; set; }
        public string FullName { get; set; }
        public string TariffName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int TariffId { get; set; }

        #endregion
    }
}
