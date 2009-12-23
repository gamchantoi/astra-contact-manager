using System;
using System.Collections.Generic;
using AutoMapper;
using ContactManager.Models;
using ContactManager.Models.Validation;
using ContactManager.Users.Interfaces;
using ContactManager.Users.Models;
using ContactManager.Users.ViewModels;

namespace ContactManager.Users.Services
{
    class ClientService : IClientService
    {
        private readonly IValidationDictionary _validationDictionary;
        private readonly IClientRepository _repository;
        private readonly IStatusService _statusService;

        #region Constructors
        public ClientService(IValidationDictionary validationDictionary)
        {
            _validationDictionary = validationDictionary;
            _repository = new EntityClientRepository();
            _statusService = new StatusService(validationDictionary);
        } 
        #endregion

        #region IClientService Members

        public bool CreateClient(Client client)
        {
            try
            {
                client.Status = _statusService.GetStatus(client.StatusId);
                _repository.CreateClient(client);
                return true;
            }
            catch (Exception ex)
            {
                _validationDictionary.AddError("_FORM", "Client is not saved. " + ex.Message);
                return false;
            }
        }

        public bool DeleteClient(Guid id)
        {
            try
            {
                _repository.DeleteClient(id);
                return true;
            }
            catch (Exception ex)
            {
                _validationDictionary.AddError("_FORM", "Client is not deleted. " + ex.Message);
                return false;
            }            
        }

        public bool EditClient(Client client)
        {
            try
            {
                client.Status = _statusService.GetStatus(client.StatusId);
                _repository.EditClient(client);
            }
            catch (Exception ex)
            {
                _validationDictionary.AddError("_FORM", "Client is not saved. " + ex.Message);
                return false;
            }
            return true;
        }

        public Client GetClient(Guid id)
        {
            return _repository.GetClient(id);
        }

        public ClientViewModel GetViewModel(Client client)
        {
            Mapper.CreateMap<Client, ClientViewModel>();
            return Mapper.Map<Client, ClientViewModel>(client);
        }

        public Client BuildClient(PPPSecret secret) 
        {
            return new Client
                       {
                           UserName = secret.Name,
                           Password = secret.Password,
                           Role = "client"
                       };
        }

        public List<Client> ListClients(bool deleted)
        {
            var ctx = new CurrentContext();
            var _status = deleted 
                ? _statusService.GetStatus(Statuses.Inactive) 
                : _statusService.GetStatus(Statuses.Active);

            var list = _repository.ListClients(_status);
            var system = list.Find(c => c.UserId == ctx.GetSystemUser().UserId);
            list.Remove(system);
            return list;
        }

        #endregion
    }
}