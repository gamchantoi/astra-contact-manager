using System.Web.Security;
using System.Collections.Generic;
using ContactManager.Models;
using System;
using ContactManager.Users.ViewModels;

namespace ContactManager.Users.Interfaces
{
    public interface IUserService
    {
        int MinPasswordLength { get; }

        bool ValidateUser(string userName, string password);        
        bool DeleteUser(Guid id);
        bool ChangePassword(string userName, string oldPassword, string newPassword);
        string GetRoleForUser(string name);        
        void EditUser(User client);

        List<User> ListUsers(string role);
        List<User> ListUsers();
        List<ClientViewModel> ListUsersModels();

        User CreateUser(User user);
        MembershipUser GetUser(Guid id);
        User GetUser(string name);

        bool ClearAllData();
    }
}