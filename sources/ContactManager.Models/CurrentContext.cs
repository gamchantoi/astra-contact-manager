using System;
using System.Web;
using System.Linq;
using System.Web.Security;

namespace ContactManager.Models
{
    public class CurrentContext : RepositoryBase<User>
    {
        public Guid CurrentUserId
        {
            get
            {
                return new Guid(Membership.GetUser(HttpContext.Current.User.Identity.Name).ProviderUserKey.ToString());
            }
        }

        public User CurrentASPUser
        {
            get
            {
                return ObjectContext.Users.FirstOrDefault(u => u.UserId == CurrentUserId);
            }
        }

        public Client CurrentClient
        {
            get
            {
                return ObjectContext.Clients.
                    Where(c => c.UserId == CurrentUserId).FirstOrDefault();
            }
        }

        public Client GetClient(Guid userId)
        {
            return ObjectContext.Clients.FirstOrDefault(c => c.UserId == userId);
        }

        public Host GetCurrentHost()
        {
            var hostId = int.Parse(HttpContext.Current.Profile.GetPropertyValue("HostId").ToString());
            return ObjectContext.HostSet.FirstOrDefault(h => h.HostId == hostId);
        }

    }
}
