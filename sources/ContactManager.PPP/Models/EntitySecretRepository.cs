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
            secret.LastUpdatedDate = DateTime.Now;
            ObjectContext.AddToPPPSecrets(secret);
            ObjectContext.SaveChanges();
            return secret;
        }

        public void DeletePPPSecret(Guid id)
        {
            var secret = ObjectContext.PPPSecrets.Where(s => s.UserId == id).FirstOrDefault();
            if (secret == null) return;
            ObjectContext.DeleteObject(secret);
            ObjectContext.SaveChanges();
        }

        public PPPSecret EditPPPSecret(PPPSecret secret)
        {

            var _secret = ObjectContext.PPPSecrets.FirstOrDefault(s => s.UserId == secret.UserId);
            if (_secret == null)
                CreatePPPSecret(secret);
            else
            {
                secret.OldName = _secret.OldName;

                if (String.IsNullOrEmpty(_secret.OldName) && (!_secret.Name.Equals(secret.Name)))
                    secret.OldName = _secret.Name;

                ObjectContext.ApplyPropertyChanges(_secret.EntityKey.EntitySetName, secret);
                _secret.LastUpdatedDate = DateTime.Now;
                ObjectContext.SaveChanges();
            }
            return _secret;
        }

        public PPPSecret GetPPPSecret(Guid id)
        {
            var secret = ObjectContext.PPPSecrets.Where(s => s.UserId == id).FirstOrDefault();
            if (secret == null)
                return null;
            secret.ProfileReference.Load();
            //if (secret.ProfileReference.Value == null)
            //    return secret;
            //secret.Profile = secret.ProfileReference.Value.Name;
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