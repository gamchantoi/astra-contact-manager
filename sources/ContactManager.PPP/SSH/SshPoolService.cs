using System;
using System.Collections.Generic;
using ContactManager.Models;
using ContactManager.Models.Validation;
using ContactManager.PPP.Intefaces;
using ContactManager.PPP.Services;

namespace ContactManager.PPP.SSH
{
    public class SshPoolService : ISshPoolService
    {
        private readonly IValidationDictionary _validationDictionary;
        private readonly SshPoolRepository _repository;
        private readonly IPoolService _poolService;

        public SshPoolService(IValidationDictionary validationDictionary)
            : this(validationDictionary, true)
        { }

        public SshPoolService(IValidationDictionary validationDictionary, bool autoMode)
        {
            _validationDictionary = validationDictionary;
            _repository = new SshPoolRepository(autoMode);
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
                //todo : resync update logic 
                pool.OldName = null;
                _poolService.EditPool(pool);
                //---------------------------
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

        //public new bool Connect(Host host)
        //{
        //    return Connect(host.Address, host.UserName, host.UserPassword);
        //}
    }
}
