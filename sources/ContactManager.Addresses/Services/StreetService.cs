using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContactManager.Addresses.Interfaces;
using ContactManager.Addresses.Models;
using ContactManager.Models;
using ContactManager.Models.Validation;

namespace ContactManager.Addresses.Services
{
    public class StreetService:IStreetService
    {
        private readonly IValidationDictionary _validationDictionary;
        private readonly IStreetRepository _streetRepository;

        public StreetService(IValidationDictionary validationDictionary)
        {
            _validationDictionary = validationDictionary;
            _streetRepository = new EntityStreetRepository();
        }


        public List<Street> ListStreets()
        {
            return _streetRepository.ListStreets();
        }


        public bool CreateStreet(Street street)
        {
            try 
            {
                _streetRepository.CreateStreet(street);
                return true;
            }
            catch(Exception ex )
            {
                _validationDictionary.AddError("_FORM", ex.Message);
                return false;
            }
        }



        public Street GetStreet(int id)
        {
            return (_streetRepository.GetStreet(id));
        }



        public bool EditStreet(Street street)
        {
            try
            {
                _streetRepository.EditStreet(street);
                return true;
            }
            catch
            {
                return false;
            }
            
        }
    }
}
