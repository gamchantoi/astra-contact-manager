using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ContactManager.Models;

namespace ContactManager.Addresses.Interfaces
{
    interface IStreetService
    {
        List<Street> ListStreets();
        bool CreateStreet(Street street);
        Street GetStreet(int id);
        Street GetStreet(string name);
        bool EditStreet(Street street);
    }
}
