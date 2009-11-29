using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ContactManager.Models;

namespace ContactManager.Intefaces
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
