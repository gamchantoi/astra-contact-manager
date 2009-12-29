using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ContactManager.Models;

namespace ContactManager.Addresses.Interfaces
{
    interface IStreetRepository
    {
        Street GetStreet(int id);
        Street EditStreet(Street street);
        Street CreateStreet(Street street);
        List<Street> ListStreets();
    }
}
