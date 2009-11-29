using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactManager.Models
{
    public partial class AccountTransaction
    {
        public int MethodId { get; set; }
        public Guid ClientId { get; set; }
        public Guid UserId { get; set; }
    }
}
