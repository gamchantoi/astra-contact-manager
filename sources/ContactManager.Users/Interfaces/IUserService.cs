using System.Web.Security;
using System.Collections.Generic;
using ContactManager.Models;
using System;

namespace ContactManager.Users.Interfaces
{
    public interface IUserService
    {
        int MinPasswordLength { get; }
        //Guid CreatedUserId { get; }
        //Guid GetCurrentUserId { get; }
        //bool UserExist(string name);
        bool ValidateUser(string userName, string password);        
        bool DeleteUser(Guid id);
        bool ChangePassword(string userName, string oldPassword, string newPassword);
        string GetRoleForUser(string name);        
        void EditUser(Client client);

        List<User> ListUsers(string role);
        List<User> ListUsers();

        bool CreateUser(Client client);
        MembershipUser GetUser(Guid id);
        MembershipUser GetUser(string name);
        //MembershipUser GetCurrentUser();
        //MembershipUser GetSystemUser();

        bool ClearAllData();
    }
}