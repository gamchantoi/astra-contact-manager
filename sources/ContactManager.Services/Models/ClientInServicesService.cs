using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ContactManager.Models;
using ContactManager.Models.Enums;
using ContactManager.Models.Validation;
using ContactManager.Services.Helpers;
using ContactManager.Services.Interfaces;
using ContactManager.Services.Models;
using ContactManager.Services.ViewModels;
using ContactManager.Users.Interfaces;
using ContactManager.Users.Services;

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
        private readonly IUserFacade _userFasade;

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
            _userFasade = new UserFacade(_validationDictionary);
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

        public bool AddClientToService(Guid clientId, int serviceId)
        {
            var activity = new ClientInServices
                               {
                                   ClientId = clientId,
                                   ServiceId = serviceId,
                                   UserId = _userHelper.GetCurrentUserId
                               };

            if (CreateActivity(activity))
                return true;
            return false;
        }

        //public bool RemoveClientFromService(Guid clientId, int serviceId)
        //{
        //    var client = _userFasade.ClientService.GetClient(clientId);
        //    var services = client.LoadClientServices();
        //    if (_repository.RemoveClientFromService(services.Where(s => s.ServiceId == serviceId).FirstOrDefault()))
        //    return true;
        //    return false;
        //}

        public bool UpdateClientServices(FormCollection collection, Guid UserId)
        {
            var client = _userFasade.ClientService.GetClient(UserId);       
            var clientServices = client.LoadClientServices();               

            var services = _serviceService.ListServices(STATUSES.Active);   


            foreach (var service in services)                               
            {
                var values = collection.GetValues(service.ServiceId.ToString());
                if (values == null) continue;                                   
                                                                                
                var isActive = bool.Parse(values[0]);                           
                var _service = service;                                         
                var activity = clientServices.Where(s => s.ServiceId == _service.ServiceId).FirstOrDefault();
                if (activity == null)                                       
                {
                    if (!isActive) continue;
                    if (AddClientToService(UserId, service.ServiceId))
                        continue;      
                    if (!service.IsRegular)
                    {
                        //client.Load = -service.Cost;
                        _userFasade.ClientService.EditClient(client);
                        //_transactionService.CreateTransaction(client, service, new Guid(_accountService.GetCurrentUser().ProviderUserKey.ToString()));
                        _repository.RemoveClientFromService(activity);
                    }
                }                                           
                else
                {
                    if (isActive) continue;
                    _repository.RemoveClientFromService(activity);
                }
            }
            return true;
        }

        public ClientServicesViewModel GetClientServices(Guid id)
        {

            //var services = _repository.ListServices(null);
            var services = _serviceService.ListServices(null);


            var client = _userFasade.ClientService.GetClient(id);
            var userServices = new List<Service>();
            client.astra_ClientsInServices.Load();

            foreach (var item in client.astra_ClientsInServices)
            {
                item.astra_ServicesReference.Load();
                userServices.Add(item.astra_ServicesReference.Value);
            }

            var userServicesViewModel = new ClientServicesViewModel();

            userServicesViewModel.ListServices = services;
            userServicesViewModel.UserServices = userServices;

            return userServicesViewModel;

        }

        #endregion
    }
}