using System;

namespace ContactManager.Models
{
    public partial class ClientInServices
    {
        public Guid UserId { get; set; }
        public Guid ClientId { get; set; }
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public string UserName { get; set; }
        public string ClientName { get; set; }
    }
}
