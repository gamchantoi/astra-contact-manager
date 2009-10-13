using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ContactManager.Models;

namespace ContactManager.Intefaces
{
    public interface IClientRepository
    {
        Client CreateClient(Client client);
        void DeleteClient(Guid id);
        Client EditClient(Client client);
        Client GetClient(Guid id);
        List<Client> ListClients(bool deleted);
    }
}
