using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContactManager.Models;
using ContactManager.Models.Validation;
using ContactManager.Users.Interfaces;
using ContactManager.Users.Models;

namespace ContactManager.Users.Services
{
    public class DetailService
    {
        private IValidationDictionary _validationDictionary;
        private EntityDetailRepository _entityDetailRepository;
        private readonly IUserFasade _userFasade;

        public DetailService(IValidationDictionary validationDictionary)
        {
            _validationDictionary = validationDictionary;
            _entityDetailRepository = new EntityDetailRepository();
            _userFasade = new UserFasade(_validationDictionary);
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
            catch (Exception)
            {
                return false;
            }
        }

        public ClientDetail GetDetails(int id)
        {
            return _entityDetailRepository.GetDetails(id);
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
