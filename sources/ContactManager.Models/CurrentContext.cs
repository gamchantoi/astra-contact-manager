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

        public User GetUser(Guid userId)
        {
            var mUser = Membership.Provider.GetUser(userId, true);
            var user = ObjectContext.Users.FirstOrDefault(u => u.UserId == userId);
            user.Password = mUser.GetPassword();
            user.Email = mUser.Email;
            return user;
        }

        public Host GetCurrentHost()
        {
            var hostId = int.Parse(HttpContext.Current.Profile.GetPropertyValue("HostId").ToString());
            return ObjectContext.HostSet.FirstOrDefault(h => h.HostId == hostId);
        }

        public User GetSystemUser()
        {
            var user = Membership.GetUser("System");
            if (user == null)
            {
                MembershipCreateStatus status;
                var _user = Membership.Provider.CreateUser("System", "SystemUserPass", "", null, null, true, null, out status);

                return status != MembershipCreateStatus.Success 
                    ? null 
                    : GetUser(new Guid(_user.ProviderUserKey.ToString()));
            }

            return GetUser(new Guid(user.ProviderUserKey.ToString()));
        }

    }
}
