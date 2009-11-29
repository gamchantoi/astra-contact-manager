using System;
using ContactManager.Models;
using ContactManager.Models.Validation;
using ContactManager.Services.Helpers;
using ContactManager.Services.Interfaces;
using ContactManager.Services.Models;

namespace ContactManager.Services.Models
{
    public class ClientInServicesService : IClientInServicesService
    {
        private readonly IValidationDictionary _validationDictionary;
        private readonly IClientInServicesRepository _repository;
        private readonly UserHelper _userHelper;
        //private readonly IClientService _clientService;
        private readonly IServiceService _serviceService;
        //private readonly IAccountTransactionService _transactionService;

        #region Constructors
        public ClientInServicesService(IValidationDictionary validationDictionary)
            : this(validationDictionary, new EntityClientInServicesRepository())
        { 
        }

        public ClientInServicesService(IValidationDictionary validationDictionary,
                                             IClientInServicesRepository repository)
        {
            _validationDictionary = validationDictionary;
            _repository = repository;
            //_clientService = new ClientService(validationDictionary);
            _serviceService = new ServiceService(validationDictionary);
            //_transactionService = new AccountTransactionService(validationDictionary);
            _userHelper = new UserHelper();
        }
        #endregion

        #region IClientServiceActivitiyService Members

        public bool CreateActivity(ClientInServices activity)
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

        public bool UpdateActivity(ClientInServices activity)
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
            var activity = new ClientInServices
                               {
                                   ClientId = clientId,
                                   ServiceId = serviceId,
                                   UserId = _userHelper.CurrentUserId
                               };
            CreateActivity(activity);
            return true;
        }

        public bool DisableActivity(Guid clientId, int serviceId)
        {
            //var client = _clientService.GetClient(clientId);
            //var activity = client.LoadServicesActivities().Where(s => s.ServiceId == serviceId).FirstOrDefault();
            //activity.Active = false;
            //UpdateActivity(activity);
            return true;
        }

        public bool UpdateActivity(System.Web.Mvc.FormCollection collection, Guid UserId)
        {
            //var client = _clientService.GetClient(UserId);
            //var activities = client.LoadServicesActivities();
            //foreach (var item in activities) {
            //    item.astra_ServicesReference.Load();
            //}

            //var services = _serviceService.ListServices(Helpers.Status.Active);
            //foreach (var service in services)
            //{
            //    var values = collection.GetValues(service.ServiceId.ToString());
            //    if (values == null) continue;
            //    var isActive = bool.Parse(values[0]);

            //    var _service = service;
            //    var activity = activities.Where(s => s.ServiceId == _service.ServiceId).FirstOrDefault();
            //    if (activity == null || !activity.Active)
            //    {
            //        if (!isActive) continue;
            //        CreateActivity(UserId, service.ServiceId);
            //        if (!service.IsRegular)
            //        {
            //            client.Load = - service.Cost;
            //            _clientService.EditClient(client);
            //            _transactionService.CreateTransaction(client, service, new Guid(_accountService.GetCurrentUser().ProviderUserKey.ToString()));
            //            DisableActivity(UserId, service.ServiceId);
            //        }
            //    }
            //    else
            //    {
            //        if (isActive) continue;
            //        DisableActivity(UserId, service.ServiceId);
            //    }
            //}
            return true;
        }

        #endregion
    }
}