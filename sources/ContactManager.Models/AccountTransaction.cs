using System;

namespace ContactManager.Models
{
    public partial class AccountTransaction
    {
        public int MethodId { get; set; }
        public int? ServiceId { get; set; }
        public Guid ClientId { get; set; }
        public Guid UserId { get; set; }
        
    }
}
