using System;
using System.Collections.Generic;
using ContactManager.Models;
using ContactManager.Models.Validation;
using ContactManager.PPP.Intefaces;
using ContactManager.PPP.Models;
using ContactManager.SSH.Intefaces;
using SSHService=ContactManager.SSH.Models.SSHService;

namespace ContactManager.PPP.SSH
{
    public class SshSecretService : SSHService, ISshSecretService
    {
        private IValidationDictionary _validationDictionary;
        private SshSecretRepository _repository;
        private ISecretService _pppSecretService;

        #region Constructors
        public SshSecretService(IValidationDictionary validationDictionary)
            : base(validationDictionary)
        {
            Init(validationDictionary, null, false);
        }

        public SshSecretService(IValidationDictionary validationDictionary, bool autoMode)
            : base(validationDictionary)
        {
            Init(validationDictionary, null, autoMode);
        }

        public SshSecretService(IValidationDictionary validationDictionary, ISSHRepository repository)
            : base(validationDictionary, repository)
        {
            Init(validationDictionary, repository, false);
        }

        private void Init(IValidationDictionary validationDictionary, ISSHRepository repository, bool autoMode)
        {
            AutoMode = autoMode;
            _validationDictionary = validationDictionary;
            _repository = repository != null ? new SshSecretRepository(repository) : new SshSecretRepository(Repository);
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
