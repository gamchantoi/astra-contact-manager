using System;
using ContactManager.Models.Validation;
using ContactManager.Web.Models;

namespace ContactManager.Web.Models
{
    public class SettingsService
    {
        private readonly IValidationDictionary _validationDictionary;
        private readonly EntitySettingsRepository _entitySettingsRepository;

        public SettingsService(IValidationDictionary validationDictionary)
        {
            _validationDictionary = validationDictionary;
            _entitySettingsRepository = new EntitySettingsRepository(validationDictionary);
        }


        public bool ClearDB()
        {
            try
            {
                _entitySettingsRepository.ClearDB();
            }
            catch (Exception ex)
            {
                _validationDictionary.AddError("_FORM", "DataBase is not cleared. " + ex.Message);
                return false;
            }
            return true;
        }
    }
}