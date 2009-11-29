using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContactManager.Addresses.Models;
using ContactManager.Models;
using ContactManager.Models.Validation;

namespace ContactManager.Addresses.Services
{
    public class AddressService
    {
        private readonly IValidationDictionary _validationDictionary;
        private readonly EntityAddressRepository _addressRepository;
        private readonly EntityStreetRepository _streetRepository;

        #region Constructors
        public AddressService(IValidationDictionary validationDictionary)
            : this(validationDictionary, new AstraEntities())
        { }

        public AddressService(IValidationDictionary validationDictionary, AstraEntities entities)
        {
            _validationDictionary = validationDictionary;
            _addressRepository = new EntityAddressRepository(entities);
            _streetRepository = new EntityStreetRepository(entities);
        }
        #endregion

        public List<Address> ListAddresses()
        {
            return _addressRepository.ListAddresses();
        }

        public List<Street> ListStreets()
        {
            return _streetRepository.ListStreets();
        }

        public SelectList ListStreets(int? selectedId)
        {
            if (selectedId.HasValue)
                return new SelectList(ListStreets(), "StreetId", "Name", selectedId.Value);
            return new SelectList(ListStreets(), "StreetId", "Name");
        }

    }
}
