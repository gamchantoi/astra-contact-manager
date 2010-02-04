using System;
using System.Collections.Generic;
using ContactManager.Models;
using ContactManager.Models.Validation;
using ContactManager.Users.Interfaces;
using ContactManager.Users.Models;

namespace ContactManager.Users.Services
{
    public class DetailService
    {
        private readonly IValidationDictionary _validationDictionary;
        private readonly EntityDetailRepository _entityDetailRepository;
        private readonly IUserFacade _userFasade;

        public DetailService(IValidationDictionary validationDictionary)
        {
            _validationDictionary = validationDictionary;
            _entityDetailRepository = new EntityDetailRepository();
            _userFasade = new UserFacade(_validationDictionary);
        }
        
        public List<ClientDetail> ListDetails()
        {
            return _entityDetailRepository.ListDetails();
        }

        public bool CreateDetail(ClientDetail detail)
        {
            try
            {
                var client = _userFasade.ClientService.GetClient(detail.UserId);
                detail.astra_Clients.Add(client);
                _entityDetailRepository.CreateDetail(detail);
                return true;
            }
            catch (Exception ex)
            {
                _validationDictionary.AddError("_FORM", "Details is not saved. " + ex.Message);
                return false;
            }
        }

        public ClientDetail GetDetails(int id)
        {
            return _entityDetailRepository.GetDetails(id);
        }

        public ClientDetail GetDetails(Guid userId)
        {
            var details = _entityDetailRepository.GetDetails(userId);
            details.UserId = userId;
            return details;
        }

        public bool EditDetail(ClientDetail detail)
        {
            try
            {
                _entityDetailRepository.EditDetail(detail);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
