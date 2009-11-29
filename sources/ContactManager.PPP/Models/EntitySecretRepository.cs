using System;
using System.Linq;
using ContactManager.Models;
using ContactManager.PPP.Intefaces;

namespace ContactManager.PPP.Models
{
    public class EntitySecretRepository : ISecretRepository
    {
        public AstraEntities Entities { get; private set; }

        #region Constructors
        public EntitySecretRepository()
        {
            Entities = new AstraEntities();
        }

        public EntitySecretRepository(AstraEntities entities)
        {
            Entities = entities;
        }
        #endregion

        #region IPPPSecretRepository Members

        public PPPSecret CreatePPPSecret(PPPSecret secret)
        {
            secret.astra_Clients = Entities.ClientSet.Where(c => c.UserId == secret.UserId).FirstOrDefault();
            if(secret.Profile != null)
                secret.mkt_PPPProfiles = Entities.ProfileSet.Where(p => p.Name == secret.Profile).FirstOrDefault();
            else if(secret.ProfileId > 0)
                secret.mkt_PPPProfiles = Entities.ProfileSet.Where(p => p.ProfileId == secret.ProfileId).FirstOrDefault();
            secret.LastUpdatedDate = DateTime.Now;
            Entities.AddToPPPSecretSet(secret);
            Entities.SaveChanges();
            return secret;
        }

        public void DeletePPPSecret(Guid id)
        {
            var secret = Entities.PPPSecretSet.Where(s => s.UserId == id).FirstOrDefault();
            if (secret == null) return;
            Entities.DeleteObject(secret);
            Entities.SaveChanges();
        }

        public PPPSecret EditPPPSecret(PPPSecret secret)
        {
            var _secret = Entities.PPPSecretSet.Where(s => s.UserId == secret.UserId).FirstOrDefault();
            if (_secret == null)
                CreatePPPSecret(secret);
            else
            {
                Entities.ApplyPropertyChanges(_secret.EntityKey.EntitySetName, secret);
                _secret.astra_Clients = Entities.ClientSet.Where(c => c.UserId == secret.UserId).FirstOrDefault();
                if (secret.ProfileId > 0)
                    _secret.mkt_PPPProfiles = Entities.ProfileSet.Where(p => p.ProfileId == secret.ProfileId).FirstOrDefault();
                else if (!string.IsNullOrEmpty(secret.Profile))
                    _secret.mkt_PPPProfiles = Entities.ProfileSet.Where(p => p.Name == secret.Profile).FirstOrDefault();
                _secret.LastUpdatedDate = DateTime.Now;
                Entities.SaveChanges();
            }
            return _secret;
        }

        public PPPSecret GetPPPSecret(Guid id)
        {
            var secret = Entities.PPPSecretSet.Where(s => s.UserId == id).FirstOrDefault();
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