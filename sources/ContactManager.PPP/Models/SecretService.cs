using System;
using ContactManager.Models;
using ContactManager.Models.Validation;
using ContactManager.PPP.Intefaces;

namespace ContactManager.PPP.Models
{
    public class SecretService : ISecretService
    {
        private readonly IValidationDictionary _validationDictionary;
        private readonly ISecretRepository _secretRepository;
        private readonly IProfileService _profileService;

        #region Constructors
        public SecretService(IValidationDictionary validationDictionary)
        {
            _validationDictionary = validationDictionary;
            _secretRepository = new EntitySecretRepository();
            _profileService = new ProfileService(validationDictionary);
        }
        #endregion

        #region IPPPSecretService Members

        public bool CreatePPPSecret(PPPSecret secret)
        {
            try
            {
                _secretRepository.CreatePPPSecret(secret);
                return true;
            }
            catch (Exception ex)
            {
                _validationDictionary.AddError("_FORM", "PPP Secret is not saved. " + ex.Message);
                return false;
            }
        }

        public bool CreatePPPSecret(Client client)
        {
            CreateSecret(client);
            return true;
        }

        public bool DeletePPPSecret(Guid id)
        {
            try
            {
                _secretRepository.DeletePPPSecret(id);
            }
            catch (Exception ex)
            {
                _validationDictionary.AddError("_FORM", "PPP Secret is not deleted. " + ex.Message);
                return false;
            }
            return true;
        }

        public bool EditPPPSecret(PPPSecret secret)
        {
            try
            {
                _secretRepository.EditPPPSecret(secret);
            }
            catch (Exception ex)
            {
                _validationDictionary.AddError("_FORM", "PPP Secret is not saved. " + ex.Message);
                return false;
            }
            return true;
        }

        public bool EditPPPSecret(Client client)
        {
            var secret = GetPPPSecret(client.UserId) ?? CreateSecret(client);
            secret.ProfileId = client.ProfileId;
            secret.Disabled = client.SecretStatus.Equals("Active");
            //secret.Profile = String.Empty;
            secret.Comment = client.Comment;
            try
            {
                return EditPPPSecret(secret);
            }
            catch (Exception ex)
            {
                _validationDictionary.AddError("_FORM", "PPP Secret is not saved. " + ex.Message);
                return false;
            }
        }

        public PPPSecret GetPPPSecret(Guid id)
        {
            //var user = _accountService.GetUser(id);
            var secret = _secretRepository.GetPPPSecret(id);
            if (secret == null)
                return null;
            //secret.Name = user.UserName;
            //secret.Password = user.GetPassword();

            secret.ProfileReference.Load();
            if (secret.ProfileReference.Value != null)
                secret.Profile = secret.ProfileReference.Value;
            return secret;
        }

        public bool UpdatePPPSecretAddresses(PPPSecret secret)
        {
            var _secret = GetPPPSecret(secret.UserId);
            _secret.LocalAddress = secret.LocalAddress;
            _secret.MACAddress = secret.MACAddress;
            _secret.RemoteAddress = secret.RemoteAddress;
            _secret.DHCPAddress = secret.DHCPAddress;
            try
            {
                _secretRepository.EditPPPSecret(_secret);
                return true;
            }
            catch (Exception ex)
            {
                _validationDictionary.AddError("_FORM", "PPP Secret is not saved. " + ex.Message);
                return false;
            }            
        }

        private PPPSecret CreateSecret(Client client)
        {
            var secret = _secretRepository.GetDefaultPPPSecret();
            secret.UserId = client.UserId;
            secret.Disabled = client.SecretStatus.Equals("Active");
            secret.ProfileId = client.ProfileId;
            secret.Password = client.Password;

            CreatePPPSecret(secret);

            return secret;
        }

        #endregion
    }
}