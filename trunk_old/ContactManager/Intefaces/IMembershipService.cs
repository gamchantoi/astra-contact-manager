using System.Web.Security;
using System.Collections.Generic;
using ContactManager.Models;
using System;

namespace ContactManager.Intefaces
{
    public interface IMembershipService
    {
        int MinPasswordLength { get; }
        Guid CreatedUserId { get; }
        Guid GetCurrentUserId { get; }
        bool UserExist(string name);
        bool ValidateUser(string userName, string password);        
        bool DeleteUser(Guid id);
        bool ChangePassword(string userName, string oldPassword, string newPassword);
        string GetRoleForUser(string name);        
        void EditUser(Client client);

        List<ASPUser> ListUsers(string role);
        List<ASPUser> ListUsers();
        //List<MembershipUser> ListUsers(string role);

        MembershipCreateStatus CreateUser(string userName, string password, string email);
        MembershipCreateStatus CreateUser(Client client);
        MembershipUser GetUser(Guid id);
        MembershipUser GetUser(string name);
        MembershipUser GetCurrentUser();

        bool ClearAllData();
    }
}