using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ContactManager.Models;
using ContactManager.Models.Validation;
using System.Text.RegularExpressions;
using ContactManager.PPP.Intefaces;
using ContactManager.PPP.Models;

namespace ContactManager.PPP.Services
{
    public class PoolService : IPoolService
    {
        private readonly IValidationDictionary _validationDictionary;
        private readonly IPoolRepository _repository;
        private const string PATTERN = @"[\(*\)*\[*\]*]";

        #region Constructors
        public PoolService(IValidationDictionary validationDictionary)
            : this(validationDictionary, new EntityPoolRepository())
        { }

        public PoolService(IValidationDictionary validationDictionary, IPoolRepository repository)
        {
            _validationDictionary = validationDictionary;
            _repository = repository;
        } 
        #endregion

        public bool ValidatePool(Pool pool)
        {
            bool isValid = true;
            var r = new Regex(PATTERN, RegexOptions.Compiled);
            if (string.IsNullOrEmpty(pool.Name))
            {
                _validationDictionary.AddError("Name", "Pool Name is required.");
                isValid = false;
            }

            if (r.Match(pool.Name).Success)
            {
                _validationDictionary.AddError("Name", "Pool Name " + pool.Name + " contain not allowed symbols '()[]'.");
                isValid = false;
            }
            return isValid;
        }

        #region IPoolService Members

        public bool CreatePool(Pool pool)
        {
            // Validation logic
            if (!ValidatePool(pool))
                return false;

            // Database logic
            try
            {
                _repository.CreatePool(pool);
            }
            catch (Exception ex)
            {
                _validationDictionary.AddError("_FORM", "Pool is not saved. " + ex.Message);
                return false;
            }
            return true;
        }

        public bool DeletePool(int id)
        {
            try
            {
                _repository.DeletePool(id);
            }
            catch (Exception ex)
            {
                _validationDictionary.AddError("_FORM", "Pool is not deleted. " + ex.Message);
                return false;
            }
            return true;
        }

        public bool EditPool(Pool pool)
        {
            if (!ValidatePool(pool))
                return false;
            try
            {
                _repository.EditPool(pool);
            }
            catch (Exception ex)
            {
                _validationDictionary.AddError("_FORM", "Pool is not saved. " + ex.Message);
                return false;
            }
            return true;
        }

        public Pool GetPool(int id)
        {
            var pool = _repository.GetPool(id);
            if (pool.NextPool.HasValue && pool.NextPool.Value != 0)
                pool.NextPoolName = _repository.GetPool(int.Parse(pool.NextPool.Value.ToString())).Name;
            return pool;
        }

        public Pool GetPool(string name)
        {
            return _repository.GetPool(name);
        }

        public List<Pool> ListPools()
        {
            return _repository.ListPools();
        }

        public SelectList ListPools(int? selectedValue)
        {
            var list = ListPools();
            SelectList returnList;
            if (selectedValue.HasValue)
                returnList =  new SelectList(list, "PoolId", "Name", selectedValue.Value);
            else
                returnList = new SelectList(list, "PoolId", "Name");

            var pool = new Pool { PoolId = 0, Name = "None"};
            list.Add(pool);
            list.Sort((c1, c2) => c1.PoolId.CompareTo(c2.PoolId));
            return returnList;

        }

        public bool CreateOrEditPools(List<Pool> pools)
        {
            var poolsForUpdate = new List<Pool>();
            foreach (var pool in pools)
            {
                if (!string.IsNullOrEmpty(pool.NextPoolName))
                    poolsForUpdate.Add(pool);

                var p = GetPool(pool.Name);
                if (p == null)
                {
                    CreatePool(pool);
                    continue;
                }
                pool.PoolId = p.PoolId;
                EditPool(pool);
            }

            foreach (var pool in poolsForUpdate)
            {
                var p = GetPool(pool.Name);
                p.NextPool = GetPool(pool.NextPoolName).PoolId;
                EditPool(p);
            }
            return true;
        }

        #endregion
    }
}