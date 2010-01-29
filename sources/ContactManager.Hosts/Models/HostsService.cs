using System;
using System.Collections;
using ContactManager.Hosts.Interfaces;
using ContactManager.Models;
using ContactManager.Models.Validation;

namespace ContactManager.Hosts.Models
{
    public class HostsService : IHostsService
    {
        private readonly IValidationDictionary _validationDictionary;
        private readonly IHostsRepository _repository;

        public HostsService(IValidationDictionary validationDictionary)
            : this(validationDictionary, new AstraEntities())
        {
        }

        public HostsService(IValidationDictionary validationDictionary, AstraEntities entities)
        {
            _validationDictionary = validationDictionary;
            _repository = new HostsRepository(entities);
        }


        #region IHostsService CreateHost(Host host)

        public bool CreateHost(Host host)
        {
            try
            {
                _repository.CreateHost(host);
                return true;
            }
            catch (Exception ex)
            {
                _validationDictionary.AddError("_FORM", "Host is not saved. " + ex.Message);
                return false;
            }
            
        }

        #endregion


        #region IHostsService DeleteHost(int id)

        public bool DeleteHost(int id)
        {
            try
            {
                _repository.DeleteHost(id);
                return true;
            }
            catch (Exception ex)
            {
                _validationDictionary.AddError("_FORM", "Host is not Deleted. " + ex.Message);
                return false;
            }
            
        }

        #endregion


        #region IHostsService EditHost(Host host)

        public bool EditHost(Host host)
        {
            try
            {
                _repository.EditHost(host);
                return true;
            }
            catch (Exception ex)
            {
                _validationDictionary.AddError("_FORM", "Host is not Edited. " + ex.Message);
                return false;
            }
        }

        #endregion


        #region IHostsService GetHost(int id)

        public Host GetHost(int id)
        {
            return _repository.GetHost(id);
        }

        #endregion


        #region IHostsService ListHosts()

        public IEnumerable ListHosts()
        {
            return _repository.ListHosts();
        }

        #endregion
    }
}
