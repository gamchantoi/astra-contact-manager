using System;
using System.Web;

namespace ContactManager.Services.Helpers
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
    }
}
