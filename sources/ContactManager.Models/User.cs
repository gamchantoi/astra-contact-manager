using System.Web.Security;
using ContactManager.Models.Enums;

namespace ContactManager.Models
{
    public partial class User
    {
        public string Role { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public bool IsInRole(ROLES role)
        {
            var roles = Roles.GetRolesForUser(UserName);
            foreach (var r in roles)
            {
                if (r.Equals(role.ToString()))
                    return true;
            }
            return false;
        }
    }
}
