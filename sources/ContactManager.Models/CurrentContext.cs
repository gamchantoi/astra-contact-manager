using System;
using System.Web;
using System.Linq;
using System.Web.Security;

namespace ContactManager.Models
{
    public class CurrentContext : RepositoryBase<ASPUser>
    {
        public Guid CurrentUserId
        {
            get
            {
                return new Guid(Membership.GetUser(HttpContext.Current.User.Identity.Name).ProviderUserKey.ToString());
            }
        }

        public ASPUser CurrentASPUser
        {
            get
            {
                return ObjectContext.ASPUserSet.
                    Where(u => u.UserId == CurrentUserId).FirstOrDefault();
            }
        }

        public Client CurrentClient
        {
            get
            {
                return ObjectContext.ClientSet.
                    Where(c => c.UserId == CurrentUserId).FirstOrDefault();
            }
        }

        public Client GetClient(Guid userId)
        {
            return ObjectContext.ClientSet.Where(c => c.UserId == userId).FirstOrDefault();
        }

    }
}
