using System;
using System.Collections.Generic;
using ContactManager.Models;
using ContactManager.Models.Validation;
using ContactManager.Users.Interfaces;
using ContactManager.Users.Models;

namespace ContactManager.Users.Services
{
    class ClientService : IClientService
    {
        private readonly IValidationDictionary _validationDictionary;
        private readonly IClientRepository _repository;
        private readonly IStatusService _statusService;

        #region Constructors
        public ClientService(IValidationDictionary validationDictionary)
            : this(validationDictionary, new AstraEntities())
        { }

        public ClientService(IValidationDictionary validationDictionary, 
            AstraEntities entities)
        {
            _validationDictionary = validationDictionary;
            _repository = new EntityClientRepository(entities);
            _statusService = new StatusService(validationDictionary, entities);
        } 
        #endregion

        #region IClientService Members

        public bool CreateClient(Client client)
        {
            try
            {
                client.astra_Statuses = _statusService.GetStatus(client.StatusId);
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
            return _repository.ListClients(deleted);
        }

        #endregion
    }
}