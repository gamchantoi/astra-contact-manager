using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Security;
using ContactManager.Models;
using ContactManager.Users.ViewModels;
using Microsoft.Data.Extensions;

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
            var mUser = Provider.GetUser(userId, true);
            var user = ObjectContext.Users.FirstOrDefault(u => u.UserId == userId);
            user.Password = mUser.GetPassword();
            user.Email = mUser.Email;
            return user;
        }

        public User GetUser(string name)
        {
            return ObjectContext.Users.FirstOrDefault(u => u.UserName == name);
        }

        public List<User> ListUser()
        {
            return ObjectContext.Users.ToList();
        }

        public List<ClientViewModel> ListModels()
        {
            var users = new List<ClientViewModel>();
 
            var command = ObjectContext.CreateStoreCommand("astra_Users_List", CommandType.StoredProcedure);
            
            using (command.Connection.CreateConnectionScope())
            using (var dataReader = command.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    var model = new ClientViewModel
                                    {
                                        UserId = !dataReader.IsDBNull(0) ? dataReader.GetGuid(0) : Guid.Empty,
                                        UserName = !dataReader.IsDBNull(1) ? dataReader.GetString(1) : "",
                                        Balance = !dataReader.IsDBNull(2) ? dataReader.GetDecimal(2) : decimal.Zero,
                                        Role = !dataReader.IsDBNull(3) ? dataReader.GetString(3) : "",
                                        ProfileId = !dataReader.IsDBNull(4) ? dataReader.GetInt32(4) : 0,
                                        ProfileName = !dataReader.IsDBNull(5) ? dataReader.GetString(5) : "",
                                        StatusDisplayName = !dataReader.IsDBNull(9) ? dataReader.GetString(9) : ""
                                    };

                    model.FullName = string.Format("{0} {1} {2}", 
                        !dataReader.IsDBNull(6) ? dataReader.GetString(6) : "", 
                        !dataReader.IsDBNull(7) ? dataReader.GetString(7) : "",
                        !dataReader.IsDBNull(8) ? dataReader.GetString(8) : "")
                        .Trim();

                    users.Add(model);
                }
            }

            return users;
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
