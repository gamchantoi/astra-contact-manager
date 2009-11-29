using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContactManager.Intefaces;

namespace ContactManager.Models
{
    public class Profile
    {
        public String TargetAddresses { get; set; }
        public String Comment { get; set; }
        public String Status { get; set; }
        public List<Guid> Clients { get; set; }
    }
}
