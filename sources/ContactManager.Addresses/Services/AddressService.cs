using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ContactManager.Addresses.Interfaces;
using ContactManager.Addresses.Models;
using ContactManager.Models;
using ContactManager.Models.Validation;

namespace ContactManager.Addresses.Services
{
    public class AddressService : IAddressService
    {
        private readonly IValidationDictionary _validationDictionary;
        private readonly EntityAddressRepository _addressRepository;
        private readonly EntityStreetRepository _streetRepository;
        private CurrentContext _ctx;

        #region Constructors
        public AddressService(IValidationDictionary validationDictionary)
        {
            _validationDictionary = validationDictionary;
            _addressRepository = new EntityAddressRepository();
            _streetRepository = new EntityStreetRepository();
            _ctx = new CurrentContext();
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

        public Street GetStreet(int id)
        {
            return _streetRepository.GetStreet(id);
        }

        public bool CreateAddress(Address address)
        {
            try
            {
                address.Street = GetStreet(address.Street.StreetId);
                var client = _ctx.GetClient(address.UserId);
                address.Client.Add(client);
                _addressRepository.Create(address);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool EditAddress(Address address)
        {
            try
            {
                _addressRepository.Edit(address);
                return true;
            }
            catch (Exception ex)
            {
                _validationDictionary.AddError("_FORM", "Address is not Edited. " + ex.Message);
                return false;
            }
        }

        public Address GetAddress(int id)
        {
            var address = _addressRepository.GetAddress(id);
            address.LoadStreetReferences();
            return address;
        }

        public Address GetAddress(Guid id)
        {
            var addres = _addressRepository.GetAddress(id);

            if (addres != null)
            {
                addres.StreetReference.Load();
                addres.UserId = id;
            }
            return addres;
        }
    }
}
