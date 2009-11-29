using System;
using System.Collections.Generic;
using ContactManager.Intefaces;
using ContactManager.Models.Validation;

namespace ContactManager.Models
{
    public class SSHService : ISSHService
    {
        private readonly IValidationDictionary _validationDictionary;
        private readonly ISSHRepository _repository;
        private readonly IPPPSecretService _pppSecretService;
        private readonly IProfileService _pppProfileService;
        private readonly IPoolService _poolService;

        #region Constructors
        public SSHService(IValidationDictionary validationDictionary)
            : this(validationDictionary,
            new EntitySSHRepository(),
            new PPPSecretService(validationDictionary),
            new ProfileService(validationDictionary),
            new PoolService(validationDictionary))
        { }

        public SSHService(IValidationDictionary validationDictionary, ISSHRepository repository,
            IPPPSecretService pppSecretService, IProfileService pppProfileService,
            IPoolService poolService)
        {
            _validationDictionary = validationDictionary;
            _repository = repository;
            _pppSecretService = pppSecretService;
            _pppProfileService = pppProfileService;
            _poolService = poolService;
        }
        #endregion

        #region ISSHService Members

        public bool Connect(string host, string username, string password)
        {
            try
            {
                _repository.Connect(host, username, password);
            }
            catch
            {
                _validationDictionary.AddError("_FORM", "SSH connection error.");
                return false;
            }
            return true;
        }

        public bool Connect(Host host)
        {
            return Connect(host.Address, host.UserName, host.UserPassword);
        }

        public void Disconnect()
        {
            _repository.Disconnect();
        }

        #region PPP SECRETS
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
        #endregion

        #region PPP PROFILES
        public bool CreatePPPProfile(int id)
        {
            try
            {
                var profile = _pppProfileService.GetProfile(id);
                _repository.ppp_profile_add(profile);
            }
            catch (Exception ex)
            {
                _validationDictionary.AddError("_FORM", "PPP Profile is not saved on host. " + ex.Message);
                return false;
            }
            return true;
        }

        public bool EditPPPProfile(int id)
        {
            try
            {
                var profile = _pppProfileService.GetProfile(id);
                _repository.ppp_profile_set(profile);
            }
            catch (Exception ex)
            {
                _validationDictionary.AddError("_FORM", "PPP Profile is not saved on host. " + ex.Message);
                return false;
            }
            return true;
        }

        public bool DeletePPPProfile(int id)
        {
            var profile = _pppProfileService.GetProfile(id);
            return DeletePPPProfile(profile.Name);
        }

        public bool DeletePPPProfile(string name)
        {
            try
            {
                _repository.ppp_profile_remove(name);
            }
            catch (Exception ex)
            {
                _validationDictionary.AddError("_FORM", name + " is not deleted on host. " + ex.Message);
                return false;
            }
            return true;
        }

        public List<Profile> ListPPPProfiles()
        {
            return _repository.ppp_profile_print();
        }
        #endregion

        #region POOLS
        public bool CreatePool(int id)
        {
            try
            {
                var pool = _poolService.GetPool(id);
                _repository.ip_pool_add(pool);
            }
            catch (Exception ex)
            {
                _validationDictionary.AddError("_FORM", "Pool is not saved on host. " + ex.Message);
                return false;
            }
            return true;
        }

        public bool EditPool(int id)
        {
            try
            {
                var pool = _poolService.GetPool(id);
                _repository.ip_pool_set(pool);
            }
            catch (Exception ex)
            {
                _validationDictionary.AddError("_FORM", "IP Pool is not saved on host. " + ex.Message);
                return false;
            }
            return true;
        }

        public bool DeletePool(int id)
        {
            var pool = _poolService.GetPool(id);
            return DeletePool(pool.Name);
        }

        public bool DeletePool(string name)
        {
            try
            {
                _repository.ip_pool_remove(name);
            }
            catch (Exception ex)
            {
                _validationDictionary.AddError("_FORM", name + " is not deleted on host. " + ex.Message);
                return false;
            }
            return true;
        }

        public List<Pool> ListPools()
        {
            return _repository.ip_pool_print();
        }
        #endregion

        public bool CacheCommit()
        {
            _repository.ppp_secret_add_cache_commit();
            return true;
        }

        public void CacheBegin()
        {
            _repository.CacheBegin("/ppp secret ");
        }

        public string system_script_add(string name, string source)
        {
            return _repository.system_script_add(name, source);
        }

        public bool system_script_run(string name)
        {
            return _repository.system_script_run(name);
        }

        #region SERVICES
        public bool UpdateServices(PPPSecret secret)
        {
            var _secret = _pppSecretService.GetPPPSecret(secret.UserId);
            if(secret.SystemRealIP)
            {
                _repository.ppp_secret_set(_secret);
                
                //_repository.ip_firewall_nat_add(_secret);
                //_repository.queue_simple_add(_secret);
            }else
            {
                _repository.ppp_secret_set(_secret);
                //_repository.ip_firewall_nat_remove(_secret);
                //_repository.queue_simple_remove(_secret);
            }
            return true;
        }
        #endregion

        #endregion
    }
}
