using System;
using ContactManager.Models;
using ContactManager.Models.Validation;
using ContactManager.Services.Interfaces;
using ContactManager.Services.Models;

namespace ContactManager.Services.Models
{
    public class SystemServicesService : ISystemServicesService
    {
        private IValidationDictionary _validationDictionary;
        private readonly ISystemServicesRepository _repository;
        private const string REAL_IP = "Real_IP_Address";
        private const string STAY_ONLINE = "Stay_OnLine";

        #region Constructors
        public SystemServicesService(IValidationDictionary validationDictionary)
            : this(validationDictionary, new EntitySystemServicesRepository())
        {
        }

        public SystemServicesService(IValidationDictionary validationDictionary, ISystemServicesRepository repository)
        {
            _validationDictionary = validationDictionary;
            _repository = repository;
        } 
        #endregion

        #region ISystemServicesService Members

        public bool AddRealIp(int serviceId)
        {
            var service = _repository.GetService(REAL_IP);
            return _repository.RegisterService(service, serviceId);
        }

        public bool AddStayOnline(int serviceId)
        {
            var service = _repository.GetService(STAY_ONLINE);
            return _repository.RegisterService(service, serviceId);
        }

        public bool RemoveRealIp(int serviceId)
        {
            throw new NotImplementedException();
        }

        public bool RemoveStayOnline(int serviceId)
        {
            throw new NotImplementedException();
        }

        public bool HasRealIp(Service service)
        {
            var _service = _repository.GetService(REAL_IP);
            service.astra_SystemServices.Load();
            if (service.astra_SystemServices.Contains(_service))
                return true;
            return false;
        }

        public bool HasStayOnline(Service service)
        {
            var _service = _repository.GetService(STAY_ONLINE);
            service.astra_SystemServices.Load();
            if (service.astra_SystemServices.Contains(_service))
                return true;
            return false;
        }

        #endregion
    }
}