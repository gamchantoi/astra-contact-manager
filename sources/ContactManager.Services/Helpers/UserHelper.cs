using System;
using System.Web;
using System.Web.Security;

namespace ContactManager.Services.Helpers
{
    public class UserHelper
    {
        public MembershipUser GetCurrentUser()
        {
            return Membership.GetUser(HttpContext.Current.User.Identity.Name);
        }

        public Guid GetCurrentUserId
        {
            get
            {
                var user = GetCurrentUser();
                return new Guid(user.ProviderUserKey.ToString());
            }
        }
    }

}
