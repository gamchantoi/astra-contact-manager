﻿using System;
using System.Collections.Generic;
using System.Linq;
using ContactManager.Models;

namespace ContactManager.Users.Models
{
    public class EntityDetailRepository:RepositoryBase<ClientDetail>
    {
        public List<ClientDetail> ListDetails()
        {
            return ObjectContext.ClientDetailSet.ToList();
        }

        public void CreateDetail(ClientDetail detail)
        {
            detail.LastUpdatedDate = DateTime.Now;
            ObjectContext.AddToClientDetailSet(detail);
            ObjectContext.SaveChanges();
        }

        public ClientDetail GetDetails(int id)
        {
            return ObjectContext.ClientDetailSet.Where(m => m.DetailId == id).FirstOrDefault();
        }

        public ClientDetail GetDetails(Guid userId)
        {
            var client = ObjectContext.Clients.FirstOrDefault(c => c.UserId.Equals(userId));
            client.ClientDetailsReference.Load();
            return client.ClientDetails;
        }

        public void EditDetail(ClientDetail detail)
        {
            var det = ObjectContext.ClientDetailSet.FirstOrDefault(m => m.DetailId == detail.DetailId);
            detail.LastUpdatedDate = DateTime.Now;
            ObjectContext.ApplyPropertyChanges(det.EntityKey.EntitySetName, detail);
            ObjectContext.SaveChanges();

        }
    }
}
