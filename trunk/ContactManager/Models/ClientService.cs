using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContactManager.Intefaces;
using ContactManager.Models.Validation;

namespace ContactManager.Models
{
    public class ClientService : IClientService
    {
        private IValidationDictionary _validationDictionary;
        private IClientRepository _repository;

        #region Constructors
        public ClientService(IValidationDictionary validationDictionary)
            : this(validationDictionary, new EntityClientRepository())
        { }

        public ClientService(IValidationDictionary validationDictionary, IClientRepository repository)
        {
            _validationDictionary = validationDictionary;
            _repository = repository;
        } 
        #endregion

        #region IClientService Members

        public bool CreateClient(Client client)
        {
            try
            {
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
