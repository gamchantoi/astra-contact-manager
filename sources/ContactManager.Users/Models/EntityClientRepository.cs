using System;
using System.Collections.Generic;
using System.Linq;
using ContactManager.Models;
using ContactManager.Users.Interfaces;

namespace ContactManager.Users.Models
{
    public class EntityClientRepository : RepositoryBase<Client>, IClientRepository
    {
        //#region Constructors
        //public EntityClientRepository()
        //{
        //    Entities = new AstraEntities();
        //}

        //public EntityClientRepository(AstraEntities entities)
        //{
        //    Entities = entities;
        //}
        //#endregion

        #region IClientRepository Members

        //public AstraEntities Entities { get; private set; }

        public Client CreateClient(Client client)
        {
            client.aspnet_Users = ObjectContext.Users.FirstOrDefault(u => u.UserId == client.UserId);
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
            ObjectContext.SaveChanges();
        }

        public Client EditClient(Client client)
        {
            var _client = ObjectContext.Clients.Where(c => c.UserId == client.UserId).FirstOrDefault();
            if (_client == null)
                return CreateClient(client);
            else
            {
                //client.Balance = client.Balance + client.Load;
                ObjectContext.ApplyPropertyChanges(_client.EntityKey.EntitySetName, client);
                _client.LastUpdatedDate = DateTime.Now;
                ObjectContext.SaveChanges();
            }
            return _client;
        }

        public Client GetClient(Guid id)
        {
            return ObjectContext.Clients.FirstOrDefault(c => c.UserId == id);
        }

        public List<Client> ListClients(bool deleted)
        {
            var _deleted = deleted ? 0 : 1;
            //todo: add status selector
            //return _entities.ClientSet.Where(c => c.Status == _deleted).ToList();
            return ObjectContext.Clients.ToList();
        }
        
        #endregion
    }
}