using System;
using System.Collections.Generic;
using ContactManager.Models;

namespace ContactManager.Users.Interfaces
{
    public interface IClientRepository
    {
        AstraEntities Entities { get; }

        Client CreateClient(Client client);
        void DeleteClient(Guid id);
        Client EditClient(Client client);
        Client GetClient(Guid id);
        List<Client> ListClients(bool deleted);
    }
}