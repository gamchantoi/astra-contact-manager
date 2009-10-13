using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContactManager.Intefaces;

namespace ContactManager.Models
{
    public class EntityPPPSecretRepository : IPPPSecretRepository
    {
        private AstraEntities _entities;

        #region Constructors
        public EntityPPPSecretRepository()
        {
            _entities = new AstraEntities();
        }

        public EntityPPPSecretRepository(AstraEntities entities)
        {
            _entities = entities;
        }
        #endregion

        #region IPPPSecretRepository Members

        public PPPSecret CreatePPPSecret(PPPSecret secret)
        {
            secret.astra_Clients = _entities.ClientSet.Where(c => c.UserId == secret.UserId).FirstOrDefault();
            if(secret.Profile != null)
                secret.mkt_PPPProfiles = _entities.ProfileSet.Where(p => p.Name == secret.Profile).FirstOrDefault();
            else if(secret.ProfileId > 0)
                secret.mkt_PPPProfiles = _entities.ProfileSet.Where(p => p.ProfileId == secret.ProfileId).FirstOrDefault();
            secret.LastUpdatedDate = DateTime.Now;
            _entities.AddToPPPSecretSet(secret);
            _entities.SaveChanges();
            return secret;
        }

        public void DeletePPPSecret(Guid id)
        {
            var secret = _entities.PPPSecretSet.Where(s => s.UserId == id).FirstOrDefault();
            if (secret == null) return;
            _entities.DeleteObject(secret);
            _entities.SaveChanges();
        }

        public PPPSecret EditPPPSecret(PPPSecret secret)
        {
            var _secret = _entities.PPPSecretSet.Where(s => s.UserId == secret.UserId).FirstOrDefault();
            if (_secret == null)
                CreatePPPSecret(secret);
            else
            {
                _entities.ApplyPropertyChanges(_secret.EntityKey.EntitySetName, secret);
                _secret.astra_Clients = _entities.ClientSet.Where(c => c.UserId == secret.UserId).FirstOrDefault();
                if (secret.ProfileId > 0)
                    _secret.mkt_PPPProfiles = _entities.ProfileSet.Where(p => p.ProfileId == secret.ProfileId).FirstOrDefault();
                else if (!string.IsNullOrEmpty(secret.Profile))
                    _secret.mkt_PPPProfiles = _entities.ProfileSet.Where(p => p.Name == secret.Profile).FirstOrDefault();
                _secret.LastUpdatedDate = DateTime.Now;
                _entities.SaveChanges();
            }
            return _secret;
        }

        public PPPSecret GetPPPSecret(Guid id)
        {
            var secret = _entities.PPPSecretSet.Where(s => s.UserId == id).FirstOrDefault();
            if (secret == null)
                return null;
            secret.mkt_PPPProfilesReference.Load();
            if (secret.mkt_PPPProfilesReference.Value == null)
                return secret;
            secret.Profile = secret.mkt_PPPProfilesReference.Value.Name;
            return secret;
        }

        public PPPSecret GetPPPSecret(string name)
        {
            throw new NotImplementedException();
        }

        public PPPSecret GetDefaultPPPSecret()
        {
            return new PPPSecret();
        }

        #endregion
    }
}
