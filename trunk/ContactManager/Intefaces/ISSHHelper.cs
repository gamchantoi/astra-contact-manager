using System.Collections.Generic;
using ContactManager.Models;

namespace ContactManager.Intefaces
{
    interface ISSHHelper
    {
        List<User> GetUsers();
        void CreateUser(User user);
        void UpdateUser(User user, out IValidatorStatus status);
    }
}