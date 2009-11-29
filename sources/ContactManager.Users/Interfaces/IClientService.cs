using System;
using System.Collections.Generic;
using ContactManager.Models;

namespace ContactManager.Users.Interfaces
{
    public interface IClientService
    {
        bool CreateClient(Client client);
        bool DeleteClient(Guid id);
        bool EditClient(Client client);
        Client GetClient(Guid id);
        Client BuildClient(PPPSecret secret);
        List<Client> ListClients(bool deleted);
    }
}