using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContactManager.Models
{
    public partial class User
    {
        public string Role { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
