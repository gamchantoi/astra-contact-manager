using System.Collections.Generic;
using System.Web.Mvc;
using ContactManager.Models;

namespace ContactManager.Addresses.Interfaces
{
    public interface IAddressService
    {
        List<Address> ListAddresses();
        List<Street> ListStreets();
        SelectList ListStreets(int? selectedId);
        Street GetStreet(int id);
        bool CreateAddress(Address address);
        bool EditAddress(Address address);
        Address GetAddress(int id);
    }
}