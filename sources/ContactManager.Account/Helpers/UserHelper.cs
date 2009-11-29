using System;
using System.Web;
using System.Web.Security;

namespace ContactManager.Account.Helpers
{
    public class UserHelper
    {
        public Guid CurrentUserId
        {
            get
            {
                return new Guid(HttpContext.Current.User.Identity.Name);
            }
        }
        public bool IsUserInRole(String role)
        {
            return Roles.IsUserInRole(role);
        }
    }
}
