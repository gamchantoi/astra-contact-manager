using System;
using System.Collections.Generic;
using System.Linq;
using ContactManager.Intefaces;

namespace ContactManager.Models
{
    public class EntityClientRepository : IClientRepository
    {
        private readonly AstraEntities _entities;

        #region Constructors
        public EntityClientRepository()
        {
            _entities = new AstraEntities();
        }

        public EntityClientRepository(AstraEntities entities)
        {
            _entities = entities;
        }
        #endregion

        #region IClientRepository Members

        public Client CreateClient(Client client)
        {
            client.aspnet_Users = _entities.ASPUserSet.Where(u => u.UserId == client.UserId).FirstOrDefault();
            client.LastUpdatedDate = DateTime.Now;
            client.Balance = client.Load;
            client.Status = 1;
            _entities.AddToClientSet(client);
            _entities.SaveChanges();
            return client;
        }

        public void DeleteClient(Guid id)
        {
            var client = _entities.ClientSet.Where(c => c.UserId == id).FirstOrDefault();
            if (client == null) return;
            //_entities.DeleteObject(client);
            client.Status = 0;
            _entities.SaveChanges();
        }

        public Client EditClient(Client client)
        {
            var _client = _entities.ClientSet.Where(c => c.UserId == client.UserId).FirstOrDefault();
            if (_client == null)
                return CreateClient(client);
            else
            {
                client.Balance = client.Balance + client.Load;
                _entities.ApplyPropertyChanges(_client.EntityKey.EntitySetName, client);
                _client.LastUpdatedDate = DateTime.Now;
                _entities.SaveChanges();
            }
            return _client;
        }

        public Client GetClient(Guid id)
        {
            var client = _entities.ClientSet.Where(c => c.UserId == id).FirstOrDefault();
            if (client == null)
                client = new Client { UserId = id };
            return client;
        }

        public List<Client> ListClients(bool deleted)
        {
            var _deleted = deleted ? 0 : 1;
            return _entities.ClientSet.Where(c => c.Status == _deleted).ToList();
        }
        
        #endregion
    }
}
