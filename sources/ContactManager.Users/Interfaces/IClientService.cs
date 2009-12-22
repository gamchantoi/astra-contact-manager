using System;
using System.Collections.Generic;
using ContactManager.Models;
using ContactManager.Users.ViewModels;

namespace ContactManager.Users.Interfaces
{
    public interface IClientService
    {
        bool CreateClient(Client client);
        bool DeleteClient(Guid id);
        bool EditClient(Client client);
        Client GetClient(Guid id);
        ClientViewModel GetViewModel(Client client);
        Client BuildClient(PPPSecret secret);
        List<Client> ListClients(bool deleted);
    }
}