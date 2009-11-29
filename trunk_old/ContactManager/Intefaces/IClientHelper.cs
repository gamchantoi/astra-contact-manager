using System;
using ContactManager.Models;

namespace ContactManager.Intefaces
{
    interface IClientHelper
    {
        Client CreateClient(IUser user);
        ClientDetail CreateClientDetails(IUser user);
        Client UpdateClient(IUser user);
        ClientDetail UpdateClientDetails(IUser user);
        Client GetClient(Guid userId);
        ClientDetail GetClientDetails(Guid userId);
    }
}