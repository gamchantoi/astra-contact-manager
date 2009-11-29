using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using ContactManager.Models;
using ContactManager.Intefaces;
using System.Web;

namespace ContactManager.Helpers
{
    public class UserHelper
    {
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://msdn.microsoft.com/en-us/library/system.web.security.membershipcreatestatus.aspx for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "Username already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A username for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }

        public string[] GetRoles()
        {
            return Roles.GetAllRoles();
        }

        public bool IsUserInRole(String role)
        {
            return Roles.IsUserInRole(role);
        }

        public List<Profile> GetProfiles() 
        { 
            var entity = new AstraEntities();
            return entity.ProfileSet.ToList();
        }

        public List<Pool> GetPools()
        {
            var entity = new AstraEntities();
            var list = entity.PoolSet.ToList();
            list.Add(new Pool { PoolId = 0, Name = "No pool..." });
            return list;
        }

        public Host GetCurrentHost()
        {
            var entity = new AstraEntities();
            var hostId = int.Parse(HttpContext.Current.Profile.GetPropertyValue("HostId").ToString());
            return entity.HostSet.Where(h => h.HostId == hostId).FirstOrDefault();
        }
    }
}