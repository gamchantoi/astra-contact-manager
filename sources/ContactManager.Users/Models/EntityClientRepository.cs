using System;
using System.Collections.Generic;
using System.Linq;
using ContactManager.Models;
using ContactManager.Users.Interfaces;

namespace ContactManager.Users.Models
{
    public class EntityClientRepository : IClientRepository
    {
        #region Constructors
        public EntityClientRepository()
        {
            Entities = new AstraEntities();
        }

        public EntityClientRepository(AstraEntities entities)
        {
            Entities = entities;
        }
        #endregion

        #region IClientRepository Members

        public AstraEntities Entities { get; private set; }

        public Client CreateClient(Client client)
        {
            client.aspnet_Users = Entities.ASPUserSet.Where(u => u.UserId == client.UserId).FirstOrDefault();
            client.LastUpdatedDate = DateTime.Now;
            client.Balance = client.Load;

            Entities.AddToClientSet(client);
            Entities.SaveChanges();
            return client;
        }

        public void DeleteClient(Guid id)
        {
            var client = Entities.ClientSet.Where(c => c.UserId == id).FirstOrDefault();
            if (client == null) return;
            //_entities.DeleteObject(client);
            //todo: add status seter
            //client.Status = 0;
            Entities.SaveChanges();
        }

        public Client EditClient(Client client)
        {
            var _client = Entities.ClientSet.Where(c => c.UserId == client.UserId).FirstOrDefault();
            if (_client == null)
                return CreateClient(client);
            else
            {
                //client.Balance = client.Balance + client.Load;
                Entities.ApplyPropertyChanges(_client.EntityKey.EntitySetName, client);
                _client.LastUpdatedDate = DateTime.Now;
                Entities.SaveChanges();
            }
            return _client;
        }

        public Client GetClient(Guid id)
        {
            var client = Entities.ClientSet.Where(c => c.UserId == id).FirstOrDefault();
            if (client == null)
                client = new Client { UserId = id };
            return client;
        }

        public List<Client> ListClients(bool deleted)
        {
            var _deleted = deleted ? 0 : 1;
            //todo: add status selector
            //return _entities.ClientSet.Where(c => c.Status == _deleted).ToList();
            return Entities.ClientSet.ToList();
        }
        
        #endregion
    }
}