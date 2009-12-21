using System;
using System.Linq;
using ContactManager.Models;
using ContactManager.PPP.Intefaces;

namespace ContactManager.PPP.Models
{
    public class EntitySecretRepository : RepositoryBase<PPPSecret>, ISecretRepository
    {
        #region IPPPSecretRepository Members

        public PPPSecret CreatePPPSecret(PPPSecret secret)
        {
            secret.astra_Clients = ObjectContext.Clients.Where(c => c.UserId == secret.UserId).FirstOrDefault();
            if(secret.Profile != null)
                secret.mkt_PPPProfiles = ObjectContext.ProfileSet.Where(p => p.Name == secret.Profile).FirstOrDefault();
            else if(secret.ProfileId > 0)
                secret.mkt_PPPProfiles = ObjectContext.ProfileSet.Where(p => p.ProfileId == secret.ProfileId).FirstOrDefault();
            secret.LastUpdatedDate = DateTime.Now;
            ObjectContext.AddToPPPSecretSet(secret);
            ObjectContext.SaveChanges();
            return secret;
        }

        public void DeletePPPSecret(Guid id)
        {
            var secret = ObjectContext.PPPSecretSet.Where(s => s.UserId == id).FirstOrDefault();
            if (secret == null) return;
            ObjectContext.DeleteObject(secret);
            ObjectContext.SaveChanges();
        }

        public PPPSecret EditPPPSecret(PPPSecret secret)
        {
            var _secret = ObjectContext.PPPSecretSet.Where(s => s.UserId == secret.UserId).FirstOrDefault();
            if (_secret == null)
                CreatePPPSecret(secret);
            else
            {
                ObjectContext.ApplyPropertyChanges(_secret.EntityKey.EntitySetName, secret);
                _secret.astra_Clients = ObjectContext.Clients.Where(c => c.UserId == secret.UserId).FirstOrDefault();
                if (secret.ProfileId > 0)
                    _secret.mkt_PPPProfiles = ObjectContext.ProfileSet.Where(p => p.ProfileId == secret.ProfileId).FirstOrDefault();
                else if (!string.IsNullOrEmpty(secret.Profile))
                    _secret.mkt_PPPProfiles = ObjectContext.ProfileSet.Where(p => p.Name == secret.Profile).FirstOrDefault();
                _secret.LastUpdatedDate = DateTime.Now;
                ObjectContext.SaveChanges();
            }
            return _secret;
        }

        public PPPSecret GetPPPSecret(Guid id)
        {
            var secret = ObjectContext.PPPSecretSet.Where(s => s.UserId == id).FirstOrDefault();
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