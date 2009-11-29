using System;
using ContactManager.Intefaces;
using ContactManager.Models.Validation;

namespace ContactManager.Models
{
    public class PPPSecretService : IPPPSecretService
    {
        private readonly IValidationDictionary _validationDictionary;
        private readonly IPPPSecretRepository _repository;
        private readonly IMembershipService _accountService;

        #region Constructors
        public PPPSecretService(IValidationDictionary validationDictionary)
            : this(validationDictionary, new EntityPPPSecretRepository())
        { }

        public PPPSecretService(IValidationDictionary validationDictionary, IPPPSecretRepository repository)
        {
            _validationDictionary = validationDictionary;
            _repository = repository;
            _accountService = new AccountMembershipService();
        }
        #endregion

        #region IPPPSecretService Members

        public bool CreatePPPSecret(PPPSecret secret)
        {
            try
            {
                _repository.CreatePPPSecret(secret);
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
            var secret = _repository.GetDefaultPPPSecret();
            secret.UserId = client.UserId;
            secret.Status = client.SecretStatus.Equals("Active") ? 1 : 0;
            secret.ProfileId = client.ProfileId;

            try
            {
                return CreatePPPSecret(secret);
            }
            catch (Exception ex)
            {
                _validationDictionary.AddError("_FORM", "PPP Secret is not saved. " + ex.Message);
                return false;
            }
        }

        public bool DeletePPPSecret(Guid id)
        {
            try
            {
                _repository.DeletePPPSecret(id);
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
                _repository.EditPPPSecret(secret);
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
            var secret = GetPPPSecret(client.UserId) ?? new PPPSecret
                                                            {
                                                                UserId = client.UserId,
                                                                ProfileId = client.ProfileId,
                                                                Name = client.UserName,
                                                                Password = client.Password,
                                                            };
            secret.ProfileId = client.ProfileId;
            secret.Status = client.SecretStatus.Equals("Active") ? 1 : 0;
            secret.Profile = String.Empty;
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
            var user = _accountService.GetUser(id);
            var secret = _repository.GetPPPSecret(id);
            if (secret == null)
                return null;
            secret.Name = user.UserName;
            secret.Password = user.GetPassword();

            secret.mkt_PPPProfilesReference.Load();
            if (secret.mkt_PPPProfilesReference.Value != null)
                secret.Profile = secret.mkt_PPPProfilesReference.Value.Name;
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
                _repository.EditPPPSecret(_secret);
                return true;
            }
            catch (Exception ex)
            {
                _validationDictionary.AddError("_FORM", "PPP Secret is not saved. " + ex.Message);
                return false;
            }            
        }

        #endregion
    }
}
