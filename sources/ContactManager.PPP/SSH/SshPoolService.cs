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
    public class SshPoolService : SSHService, ISshPoolService
    {
        private readonly IValidationDictionary _validationDictionary;
        private readonly SshPoolRepository _repository;
        private readonly IPoolService _poolService;

        public SshPoolService(IValidationDictionary validationDictionary) : base(validationDictionary)
        {
            _validationDictionary = validationDictionary;
            _repository = new SshPoolRepository();
            _poolService = new PoolService(validationDictionary);
        }

        public SshPoolService(IValidationDictionary validationDictionary, ISSHRepository repository) : base(validationDictionary, repository)
        {
            _validationDictionary = validationDictionary;
            _repository = new SshPoolRepository(repository);
            _poolService = new PoolService(validationDictionary);
        }

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

        public bool Connect(ContactManager.Models.Host host)
        {
            return Connect(host.Address, host.UserName, host.UserPassword);
        }
    }
}
