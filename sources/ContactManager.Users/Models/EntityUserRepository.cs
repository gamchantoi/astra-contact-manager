using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using ContactManager.Models;

namespace ContactManager.Users.Models
{
    public class EntityUserRepository : RepositoryBase<User>
    {
        public MembershipProvider Provider { get; private set; }

        public EntityUserRepository()
        {
            Provider = Membership.Provider;
        }

        public User CreateUser(User user)
        {
            MembershipCreateStatus status;
            var _user = Provider.CreateUser(user.UserName, user.Password, user.Email, null, null, true, null, out status);
            var userId = new Guid(_user.ProviderUserKey.ToString());

            if (status != MembershipCreateStatus.Success) return null;

            Roles.AddUserToRole(user.UserName, user.Role);
            return GetUser(userId);
        }

        public User GetUser(Guid userId)
        {
            return ObjectContext.Users.FirstOrDefault(u => u.UserId == userId);
        }

        public User GetUser(string name)
        {
            return ObjectContext.Users.FirstOrDefault(u => u.UserName == name);
        }

        public List<User> ListUser()
        {
            return ObjectContext.Users.ToList();
        }

        public List<User> ListUser(string role)
        {
            var _users = new List<User>();
            var users = Roles.GetUsersInRole(role);
            foreach (var user in users)
            {
                var _user = user;
                _users.Add(ObjectContext.Users.Where(u => u.UserName == _user).FirstOrDefault());
            }
            return _users;
        }

        public User EditUser(User user)
        {
            var mUser = Provider.GetUser(user.UserId, true);

            if (!String.IsNullOrEmpty(user.Email)) mUser.Email = user.Email;

            if (!String.Equals(mUser.GetPassword(), user.Password)
                && !string.IsNullOrEmpty(user.Password))

                mUser.ChangePassword(mUser.GetPassword(), user.Password);
            Provider.UpdateUser(mUser);

            var _user = GetUser(user.UserId);

            if (!_user.UserName.Equals(user.UserName))
            {
                _user.UserName = user.UserName;
                _user.LoweredUserName = user.UserName.ToLower();
                ObjectContext.SaveChanges();
            }

            return _user;
        }
    }
}
