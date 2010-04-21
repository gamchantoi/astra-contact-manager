﻿using System;
using System.Data.Objects;
using ContactManager.Models;
using ContactManager.Services.Interfaces;
using Enumerable=System.Linq.Enumerable;

namespace ContactManager.Services.Models
{
    public class EntityCustomResourcesRepository : RepositoryBase<CustomResource>, IEntityCustomResourcesRepository
    {

        private readonly AstraEntities _entities;

        #region Constructors
        public EntityCustomResourcesRepository()
        {
            _entities = new AstraEntities();
        }

        public EntityCustomResourcesRepository(AstraEntities entities)
        {
            _entities = entities;
        }
        #endregion


        public bool CreateResource(string key, string value)
        {
            var customResource = new CustomResource {Key = key, Value = value, LastActivityDate = DateTime.Now};
            ObjectContext.AddToCustomResourceSet(customResource);
            ObjectContext.SaveChanges();
            //todo: add "if" logic here
            return true;
        }

        public CustomResource EditResource(CustomResource resource)
        {
            var customResource = GetResource(resource.Key);
            ObjectContext.ApplyPropertyChanges(resource.EntityKey.EntitySetName, customResource);
            ObjectContext.SaveChanges();
            return customResource;
        }

        public CustomResource GetResource(string key)
        {
            return Enumerable.FirstOrDefault(ObjectContext.CustomResourceSet, n => n.Key.Equals(key));
        }

        public bool DeleteResource(string key)
        {
            ObjectContext.DeleteObject(GetResource(key));
            return true;
        }

    }
}
