using System;
using System.Collections.Generic;
using ContactManager.Models;
using ContactManager.Models.Validation;
using ContactManager.Services.Helpers;
using ContactManager.Services.Interfaces;
using ContactManager.Services.Models;

namespace ContactManager.Services.Models
{
    public class ServiceService : IServiceService
    {
        private readonly IValidationDictionary _validationDictionary;
        private readonly IServiceRepository _repository;
        private readonly UserHelper _userHelper;
        private readonly ISystemServicesService _systemService;
        private const string REAL_IP = "Real_IP_Address";
        private const string STAY_ONLINE = "Stay_OnLine";

        #region Constructors
        public ServiceService(IValidationDictionary validationDictionary)
            : this(validationDictionary, new EntityServiceRepository(), new SystemServicesService(validationDictionary))
        { }

        public ServiceService(IValidationDictionary validationDictionary, IServiceRepository repository,
                              ISystemServicesService systemService)
        {
            _validationDictionary = validationDictionary;
            _userHelper = new UserHelper();
            _repository = repository;
            _systemService = systemService;
        } 
        #endregion

        #region IServiceService Members

        public List<Service> ListServices(Statuses? status) 
        {
            return _repository.ListServices(status);
        }

        public bool CreateService(Service service)
        {
            try
            {
                if (_repository.GetService(service.Name) != null)
                    throw new Exception("Service Name exist.");
                service.UserId = _userHelper.CurrentUserId;
                var _service = _repository.CreateService(service);
                
                if (service.SystemRealIP)
                    _systemService.AddRealIp(_service.ServiceId);
                if (service.SystemStayOnline)
                    _systemService.AddStayOnline(_service.ServiceId);

                return true;
            }
            catch (Exception ex)
            {
                _validationDictionary.AddError("_FORM", "Service is not saved. " + ex.Message);
                return false;
            }
        }

        public Service GetService(int id)
        {
            return _repository.GetService(id);
        }

        public bool EditService(Service service)
        {
            try
            {
                var _service = _repository.GetService(service.Name);
                if (_service != null && _service.ServiceId != service.ServiceId)
                    throw new Exception("Service Name exist.");
                _repository.EditService(service);
                return true;
            }
            catch (Exception ex)
            {
                _validationDictionary.AddError("_FORM", "Service is not saved. " + ex.Message);
                return false;
            }            
        }

        public bool UpdateSystemService(PPPSecret secret)
        {
            UpdateSystemService(secret, REAL_IP, secret.SystemRealIP);
            //UpdateSystemService(secret, STAY_ONLINE, secret.SystemStayOnline);

            return true;
        }

        private void UpdateSystemService(PPPSecret secret, string serviceType, bool mode)
        {
            var _service = _repository.BuildService(serviceType);
            _service.UserId = _userHelper.CurrentUserId;
            _service.ClientId = secret.UserId;
            if (mode)
            {
                if (!_repository.ClientRegistered(_service))
                    _repository.RegisterClient(_service);
            }
            else
            {
                if (_repository.ClientRegistered(_service))
                    _repository.UnRegisterClient(_service);
            }
        }

        public List<ClientInServices> ListActivities()
        {
            return _repository.ListActivities();
        }

        #endregion
    }
}