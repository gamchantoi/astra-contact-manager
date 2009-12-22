using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContactManager.Models;

namespace ContactManager.Users.ViewModels
{
    public class ClientViewModel
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string ProfileName { get; set; }
        public decimal Balance { get; set; }
        public string Role { get; set; }
        public string StatusDisplayName { get; set; }
        public string StatusName { get; set; }

        //public Status Status { get; set; }
    }
}
