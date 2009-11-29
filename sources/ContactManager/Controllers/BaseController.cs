using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using ContactManager.Models;

namespace ContactManager.Controllers
{
    public class BaseController : Controller
    {
        private Guid _currentUserId;

        public Guid CurrentUserId
        {
            get
            {
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    var user = Membership.GetUser();
                    _currentUserId = new Guid(user.ProviderUserKey.ToString());
                    return _currentUserId;
                }
                else
                    return Guid.Empty;
            }
        }
    }
}