using System;
using ContactManager.Models;
using ContactManager.Models.Validation;
using ContactManager.Services.Interfaces;

namespace ContactManager.Services.Models
{
    public class CustomResourcesService : ICustomResourcesService
    {
        private readonly IValidationDictionary _validationDictionary;
        //private readonly UserHelper _userHelper;
        //private readonly IUserFacade _userFasade;
        private readonly IEntityCustomResourcesRepository _repository;

        #region Constructors
        public CustomResourcesService(IValidationDictionary validationDictionary)
            : this(validationDictionary, new EntityCustomResourcesRepository())
        {
        }

        public CustomResourcesService(IValidationDictionary validationDictionary, IEntityCustomResourcesRepository repository)
        {
            _validationDictionary = validationDictionary;
            _repository = repository;
            //_userHelper = new UserHelper();
            //_userFasade = new UserFacade(_validationDictionary);
        }
        #endregion
        
        public bool CreateResource(string key, string value)
        {
            if (_repository.CreateResource(key, value))
            return true;
            else
            {
                return false;
            }
        }

        public CustomResource EditResource(CustomResource resource)
        {
            return _repository.EditResource(resource);
        }

        public CustomResource GetResource(string key)
        {
            return _repository.GetResource(key);
        }

        public bool DeleteResource(string key)
        {
            if (_repository.DeleteResource(key))
                return true;
            else
            {
                return false;
            }
        }
    }
}
