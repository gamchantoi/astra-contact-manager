using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using ContactManager.Models;
using ContactManager.Users.Interfaces;

namespace ContactManager.Users.Models
{
    public class EntityClientRepository : RepositoryBase<Client>, IClientRepository
    {
        #region IClientRepository Members

        public Client CreateClient(Client client)
        {
            client.User = ObjectContext.Users.FirstOrDefault(u => u.UserId == client.UserId);
            client.LastUpdatedDate = DateTime.Now;
            client.Balance = client.Load;

            ObjectContext.AddToClients(client);
            ObjectContext.SaveChanges();
            return client;
        }

        public void DeleteClient(Guid id)
        {
            var client = ObjectContext.Clients.Where(c => c.UserId == id).FirstOrDefault();
            if (client == null) return;
            //_entities.DeleteObject(client);
            //todo: add status seter
            //client.Status = 0;
            //ObjectContext.SaveChanges();
        }

        public Client EditClient(Client client)
        {
            var _client = ObjectContext.Clients.Where(c => c.UserId == client.UserId).FirstOrDefault();
            if (_client == null)
                return CreateClient(client);

            ObjectContext.ApplyPropertyChanges(_client.EntityKey.EntitySetName, client);
            _client.LastUpdatedDate = DateTime.Now;
            ObjectContext.SaveChanges();
            return _client;
        }

        public Client GetClient(Guid id)
        {
            return ObjectContext.Clients.FirstOrDefault(c => c.UserId == id);
        }

        public List<Client> ListClients(Status status, bool inStatus)
        {
            if (inStatus)
                return ObjectContext.Clients
                    .Join(ObjectContext.Statuses,
                        c => c.Status.StatusId, s => s.StatusId,
                        (c, s) => new { c, s })
                    .Where(temp0 => (temp0.c.Status.StatusId == status.StatusId))
                    .Select(temp0 => temp0.c).ToList();

            return ObjectContext.Clients
                    .Join(ObjectContext.Statuses,
                        c => c.Status.StatusId, s => s.StatusId,
                        (c, s) => new { c, s })
                    .Where(temp0 => (temp0.c.Status.StatusId != status.StatusId))
                    .Select(temp0 => temp0.c).ToList();
        }

        #endregion
    }
}