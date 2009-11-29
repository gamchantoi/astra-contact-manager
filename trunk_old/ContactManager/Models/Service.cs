using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactManager.Models
{
    public partial class Service
    {
        public bool SystemRealIP { get; set; }
        public bool SystemStayOnline { get; set; }
        public Guid UserId { get; set; }
        public Guid ClientId { get; set; }        
    }
}
