using System.Web.Security;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Web;
using ContactManager.Models;
using ContactManager.Users.Interfaces;

namespace ContactManager.Users.Services
{
    class MembershipService : IMembershipService
    {
        private readonly MembershipProvider _provider;
        private readonly AstraEntities _entities;

        #region Constructors
        public MembershipService()
            : this(new AstraEntities()) { }

        public MembershipService(AstraEntities entities)
            : this(null, entities) { }

        public MembershipService(MembershipProvider provider, AstraEntities entities)
        {
            _provider = provider ?? Membership.Provider;
            _entities = entities;
        } 
        #endregion

        #region IMembershipService Members

        public Guid CreatedUserId { get; private set; }

        public int MinPasswordLength
        {
            get
            {
                return _provider.MinRequiredPasswordLength;
            }
        }

        public bool UserExist(string name)
        {
            return _provider.GetUser(name, true) != null;
        }

        public bool ValidateUser(string userName, string password)
        {
            return _provider.ValidateUser(userName, password);
        }

        private MembershipCreateStatus CreateUser(string userName, string password, string email)
        {
            MembershipCreateStatus status;
            var user = _provider.CreateUser(userName, password, email, null, null, true, null, out status);
            CreatedUserId = new Guid(user.ProviderUserKey.ToString());
            return status;
        }

        private MembershipCreateStatus CreateUser(string userName, string password, string email, string role)
        {
            var status = CreateUser(userName, password, email);
            if (status == MembershipCreateStatus.Success)
            {
                Roles.AddUserToRole(userName, role);
                return status;
            }
            return status;
        }

        public MembershipCreateStatus CreateUser(Client client)
        {
            var status = CreateUser(client.UserName, client.Password, client.Email, client.Role);
            client.UserId = CreatedUserId;
            return status;
        }

        public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            var currentUser = _provider.GetUser(userName, true /* userIsOnline */);
            return currentUser.ChangePassword(oldPassword, newPassword);
        }

        public List<ASPUser> ListUsers(string role)
        {
            var _users = new List<ASPUser>();
            var users = Roles.GetUsersInRole(role);
            foreach (var user in users)
            {
                _users.Add(_entities.ASPUserSet.Where(u => u.UserName == user).FirstOrDefault());
                
            }
            return _users;
        }

        public List<ASPUser> ListUsers()
        {
            return _entities.ASPUserSet.ToList();
        }

        //public List<MembershipUser> ListUsers(string role)
        //{
        //    var users = new List<MembershipUser>();
        //    var usersNames = Roles.GetUsersInRole(role);
        //    foreach(var user in usersNames)
        //    {
        //        var mUser = Membership.Provider.GetUser(user, false);
        //        users.Add(mUser);
        //    }
        //    return users;
        //}

        public string GetRoleForUser(string name)
        {
            //var role = String.Empty;
            //foreach (var r in Roles.GetRolesForUser(name))
            //{
            //    role += r + " ";
            //}
            return Roles.GetRolesForUser(name)[0];
        }

        public MembershipUser GetUser(Guid id)
        {
            return Membership.Provider.GetUser(id, false);
        }

        public MembershipUser GetUser(string name)
        {
            return Membership.Provider.GetUser(name, false);
        }

        public MembershipUser GetCurrentUser() 
        { 
            return Membership.GetUser(HttpContext.Current.User.Identity.Name);
        }

        public MembershipUser GetSystemUser()
        {
            var user = Membership.GetUser("System");
            if (user == null)
                CreateUser("System", "SystemUserPass", "");
            return user;
        }

        public void EditUser(Client client)
        {
            var mUser = Membership.Provider.GetUser(client.UserId, true);

            if (!String.IsNullOrEmpty(client.Email)) mUser.Email = client.Email;

            if (!String.Equals(mUser.GetPassword(), client.Password))
                mUser.ChangePassword(mUser.GetPassword(), client.Password);
            Membership.Provider.UpdateUser(mUser);

            if (!String.IsNullOrEmpty(client.Role) && !Roles.IsUserInRole(mUser.UserName, client.Role))
            {
                Roles.RemoveUserFromRoles(mUser.UserName, Roles.GetRolesForUser(mUser.UserName));
                Roles.AddUserToRole(mUser.UserName, client.Role);
            }
        }

        public bool DeleteUser(Guid id) 
        {
            var user = GetUser(id);
            Membership.Provider.DeleteUser(user.UserName, true);
            return true;
        }

        public Guid GetCurrentUserId
        {
            get 
            {
                var user = GetCurrentUser();
                return new Guid(user.ProviderUserKey.ToString());
            }
        }

        public bool ClearAllData()
        {
            foreach (var transaction in _entities.TransactionSet.ToList())
            {
                _entities.DeleteObject(transaction);
            }
            foreach (var secret in _entities.PPPSecretSet.ToList())
            {
                _entities.DeleteObject(secret);
            }
            foreach (var profile in _entities.ProfileSet.ToList())
            {
                _entities.DeleteObject(profile);
            }
            foreach (var pool in _entities.PoolSet.ToList())
            {
                _entities.DeleteObject(pool);
            }
            foreach (var transaction in _entities.TransactionSet.ToList())
            {
                _entities.DeleteObject(transaction);
            }
            //foreach (var transactionmethod in _entities.AccountTransactionMethodSet.ToList())
            //{
            //    _entities.DeleteObject(transactionmethod);
            //}
            //foreach (var activity in _entities.ClientServiceActivitiySet.ToList())
            //{
            //    _entities.DeleteObject(activity);
            //}
            foreach (var systemservice in _entities.SystemServiceSet.ToList())
            {
                systemservice.astra_Services.Load();
                systemservice.astra_Services.Clear();
                _entities.DeleteObject(systemservice);
            }
            foreach (var service in _entities.ServiceSet.ToList())
            {
                _entities.DeleteObject(service);
            }
            foreach (var client in _entities.ClientSet.ToList())
            {
                var user = GetUser(client.UserId);
                if(GetRoleForUser(user.UserName).Equals("admin")) continue;                
                _entities.DeleteObject(client);
            }
            _entities.SaveChanges();
            foreach (var user in ListUsers("client"))
            {                
                Membership.Provider.DeleteUser(user.UserName, true);
            }

            return true;
        }

        #endregion
    }

    public enum Role
    {
        admin,
        client
    }
}