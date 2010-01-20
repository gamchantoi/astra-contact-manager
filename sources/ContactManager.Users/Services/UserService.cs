using System.Web.Security;
using System.Collections.Generic;
using System;
using ContactManager.Models;
using ContactManager.Models.Validation;
using ContactManager.Users.Interfaces;
using ContactManager.Users.Models;
using ContactManager.Users.ViewModels;

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

        public User CreateUser(User user)
        {
            try
            {
                return _repository.CreateUser(user);
            }
            catch (Exception ex)
            {
                _validationDictionary.AddError("_FORM", "User is not saved. " + ex.Message);
                return null;
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

        public List<ClientViewModel> ListUsersModels()
        {
            return _repository.ListModels();
        }

        public string GetRoleForUser(string name)
        {
            //var role = String.Empty;
            //foreach (var r in Roles.GetRolesForUser(name))
            //{
            //    role += r + " ";
            //}
            return Roles.GetRolesForUser(name)[0];
        }

        public User GetUser(Guid id)
        {
            return _repository.GetUser(id);
        }

        public User GetUser(string name)
        {
            return _repository.GetUser(name);
        }

        public void EditUser(User user)
        {
            var _user = _repository.EditUser(user);

            if (!String.IsNullOrEmpty(user.Role) && !Roles.IsUserInRole(_user.UserName, user.Role))
            {
                Roles.RemoveUserFromRoles(_user.UserName, Roles.GetRolesForUser(_user.UserName));
                Roles.AddUserToRole(_user.UserName, user.Role);
            }
        }

        public bool DeleteUser(Guid id) 
        {
            var user = GetUser(id);
            Membership.Provider.DeleteUser(user.UserName, true);
            return true;
        }
        #endregion
    }


}