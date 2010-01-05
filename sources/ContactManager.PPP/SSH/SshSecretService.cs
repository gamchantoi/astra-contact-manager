using System;
using System.Collections.Generic;
using ContactManager.Models;
using ContactManager.Models.Validation;
using ContactManager.PPP.Intefaces;
using ContactManager.PPP.Services;

namespace ContactManager.PPP.SSH
{
    public class SshSecretService : ISshSecretService
    {
        private readonly IValidationDictionary _validationDictionary;
        private readonly SshSecretRepository _repository;
        private readonly ISecretService _pppSecretService;

        #region Constructors
        public SshSecretService(IValidationDictionary validationDictionary)
            : this(validationDictionary, true)
        {
        }

        public SshSecretService(IValidationDictionary validationDictionary, bool autoMode)
        {
            _validationDictionary = validationDictionary;
            _repository = new SshSecretRepository(autoMode);
            _pppSecretService = new SecretService(validationDictionary);
        }

        #endregion

        public bool CreatePPPSecret(Guid id)
        {
            try
            {
                var secret = _pppSecretService.GetPPPSecret(id);
                _repository.ppp_secret_add(secret);
            }
            catch (Exception ex)
            {
                _validationDictionary.AddError("_FORM", "PPP Secret is not saved on host. " + ex.Message);
                return false;
            }
            return true;
        }

        public bool EditPPPSecret(Guid id)
        {
            try
            {
                var secret = _pppSecretService.GetPPPSecret(id);
                _repository.ppp_secret_set(secret);
                
                secret.OldName = null;
                _pppSecretService.EditPPPSecret(secret);
            }
            catch (Exception ex)
            {
                _validationDictionary.AddError("_FORM", "PPP Secret is not saved on host. " + ex.Message);
                return false;
            }
            return true;
        }

        public bool DeletePPPSecret(Guid id)
        {
            var secret = _pppSecretService.GetPPPSecret(id);
            return DeletePPPSecret(secret.Name);
        }

        public bool DeletePPPSecret(string name)
        {
            try
            {
                _repository.ppp_secret_remove(name);
            }
            catch (Exception ex)
            {
                _validationDictionary.AddError("_FORM", name + " is not deleted on host. " + ex.Message);
                return false;
            }
            return true;
        }

        public List<PPPSecret> ListPPPSecrets()
        {
            return _repository.ppp_secret_print();
        }
    }
}
