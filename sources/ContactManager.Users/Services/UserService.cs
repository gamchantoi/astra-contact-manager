using System.Web.Security;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Web;
using ContactManager.Models;
using ContactManager.Models.Validation;
using ContactManager.Users.Interfaces;
using ContactManager.Users.Models;

namespace ContactManager.Users.Services
{
    class UserService : IUserService
    {
        private readonly EntityUserRepository _repository;
        private readonly IValidationDictionary _validationDictionary;

        #region Constructors
        public UserService(IValidationDictionary validationDictionary)
        {
            _validationDictionary = validationDictionary;
            _repository = new EntityUserRepository();
        }

        //public UserService(MembershipProvider provider, AstraEntities entities)
        //{
        //    _provider = provider ?? Membership.Provider;
        //    _entities = entities;
        //} 
        #endregion

        #region IMembershipService Members

        public int MinPasswordLength
        {
            get
            {
                return _repository.Provider.MinRequiredPasswordLength;
            }
        }

        public bool ValidateUser(string userName, string password)
        {
            return _repository.Provider.ValidateUser(userName, password);
        }

        public bool CreateUser(Client client)
        {
            try
            {
                _repository.CreateUser(client);
                return true;
            }
            catch (Exception ex)
            {
                _validationDictionary.AddError("_FORM", "User is not saved. " + ex.Message);
                return false;
            }
        }

        public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            var currentUser = _repository.Provider.GetUser(userName, true /* userIsOnline */);
            return currentUser.ChangePassword(oldPassword, newPassword);
        }

        public List<User> ListUsers(string role)
        {
            return _repository.ListUser(role);
        }

        public List<User> ListUsers()
        {
            return _repository.ListUser();
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

        //public MembershipUser GetCurrentUser() 
        //{ 
        //    return Membership.GetUser(HttpContext.Current.User.Identity.Name);
        //}

        public void EditUser(User user)
        {
            var mUser = Membership.Provider.GetUser(user.UserId, true);

            if (!String.IsNullOrEmpty(user.Email)) mUser.Email = user.Email;

            if (!String.Equals(mUser.GetPassword(), user.Password))
                mUser.ChangePassword(mUser.GetPassword(), user.Password);
            Membership.Provider.UpdateUser(mUser);

            if (!String.IsNullOrEmpty(user.Role) && !Roles.IsUserInRole(mUser.UserName, user.Role))
            {
                Roles.RemoveUserFromRoles(mUser.UserName, Roles.GetRolesForUser(mUser.UserName));
                Roles.AddUserToRole(mUser.UserName, user.Role);
            }
        }

        public bool DeleteUser(Guid id) 
        {
            var user = GetUser(id);
            Membership.Provider.DeleteUser(user.UserName, true);
            return true;
        }

        //public Guid GetCurrentUserId
        //{
        //    get 
        //    {
        //        var user = GetCurrentUser();
        //        return new Guid(user.ProviderUserKey.ToString());
        //    }
        //}

        public bool ClearAllData()
        {
            //foreach (var transaction in _entities.Transactions.ToList())
            //{
            //    _entities.DeleteObject(transaction);
            //}
            //foreach (var secret in _entities.PPPSecretSet.ToList())
            //{
            //    _entities.DeleteObject(secret);
            //}
            //foreach (var profile in _entities.ProfileSet.ToList())
            //{
            //    _entities.DeleteObject(profile);
            //}
            //foreach (var pool in _entities.PoolSet.ToList())
            //{
            //    _entities.DeleteObject(pool);
            //}
            //foreach (var transaction in _entities.Transactions.ToList())
            //{
            //    _entities.DeleteObject(transaction);
            //}
            ////foreach (var transactionmethod in _entities.AccountTransactionMethodSet.ToList())
            ////{
            ////    _entities.DeleteObject(transactionmethod);
            ////}
            ////foreach (var activity in _entities.ClientServiceActivitiySet.ToList())
            ////{
            ////    _entities.DeleteObject(activity);
            ////}
            //foreach (var systemservice in _entities.SystemServiceSet.ToList())
            //{
            //    systemservice.astra_Services.Load();
            //    systemservice.astra_Services.Clear();
            //    _entities.DeleteObject(systemservice);
            //}
            //foreach (var service in _entities.ServiceSet.ToList())
            //{
            //    _entities.DeleteObject(service);
            //}
            //foreach (var client in _entities.ClientSet.ToList())
            //{
            //    var user = GetUser(client.UserId);
            //    if(GetRoleForUser(user.UserName).Equals("admin")) continue;                
            //    _entities.DeleteObject(client);
            //}
            //_entities.SaveChanges();
            //foreach (var user in ListUsers("client"))
            //{                
            //    Membership.Provider.DeleteUser(user.UserName, true);
            //}

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