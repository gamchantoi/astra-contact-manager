using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContactManager.Intefaces;
using ContactManager.Models.Validation;

namespace ContactManager.Models
{
    public class ClientServiceActivitiyService : IClientServiceActivitiyService
    {
        private readonly IValidationDictionary _validationDictionary;
        private readonly IClientServiceActivitiyRepository _repository;
        private readonly IMembershipService _accountService;
        private readonly IClientService _clientService;
        private readonly IServiceService _serviceService;

        #region Constructors
        public ClientServiceActivitiyService(IValidationDictionary validationDictionary)
            : this(validationDictionary, new EntityClientServiceActivitiyRepository())
        { 
        }

        public ClientServiceActivitiyService(IValidationDictionary validationDictionary,
            IClientServiceActivitiyRepository repository)
        {
            _validationDictionary = validationDictionary;
            _repository = repository;
            _accountService = new AccountMembershipService();
            _clientService = new ClientService(validationDictionary);
            _serviceService = new ServiceService(validationDictionary);
        }
        #endregion

        #region IClientServiceActivitiyService Members

        public bool CreateActivity(ClientServiceActivitiy activity)
        {
            try
            {
                _repository.CreateActivity(activity);
                return true;
            }
            catch (Exception ex)
            {
                _validationDictionary.AddError("_FORM", "Client is not saved. " + ex.Message);
                return false;
            }
        }

        public bool UpdateActivity(ClientServiceActivitiy activity)
        {
            try
            {
                _repository.UpdateActivity(activity);
                return true;
            }
            catch (Exception ex)
            {
                _validationDictionary.AddError("_FORM", "Client is not saved. " + ex.Message);
                return false;
            }
        }

        public bool CreateActivity(Guid clientId, int serviceId)
        {
            var activity = new ClientServiceActivitiy();
            activity.Active = true;
            activity.ClientId = clientId;
            activity.ServiceId = serviceId;
            activity.UserId = new Guid(_accountService.GetCurrentUser().ProviderUserKey.ToString());
            CreateActivity(activity);
            return true;
        }

        public bool DisableActivity(Guid clientId, int serviceId)
        {
            var client = _clientService.GetClient(clientId);
            var activity = client.LoadServicesActivities().Where(s => s.ServiceId == serviceId).FirstOrDefault();
            activity.Active = false;
            UpdateActivity(activity);
            return true;
        }

        public bool UpdateActivity(System.Web.Mvc.FormCollection collection, Guid UserId)
        {
            var client = _clientService.GetClient(UserId);
            var activities = client.LoadServicesActivities();
            foreach (var item in activities) {
                item.astra_ServicesReference.Load();
            }

            var services = _serviceService.ListServices(Helpers.Status.Active);
            foreach (var service in services)
            {
                var values = collection.GetValues(service.ServiceId.ToString());
                if (values == null) continue;
                var isActive = bool.Parse(values[0]);

                var _service = service;
                var activity = activities.Where(s => s.ServiceId == _service.ServiceId).FirstOrDefault();
                if (activity == null || !activity.Active)
                {
                    if (!isActive) continue;
                    CreateActivity(UserId, service.ServiceId);
                    if(!service.IsRegular)
                        DisableActivity(UserId, service.ServiceId);
                }
                else
                {
                    if (isActive) continue;
                    DisableActivity(UserId, service.ServiceId);
                }
            }
            return true;
        }

        #endregion
    }
}
