using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ContactManager.Models;

namespace ContactManager.Intefaces
{
    public interface IProfileService
    {
        bool CreateProfile(Profile profile);
        bool CreateOrEditProfiles(List<Profile> profiles);
        bool DeleteProfile(int id);
        bool EditProfile(Profile profile);
        Profile GetProfile(int id);        
        //Profile GetTariff(Profile tariff);
        List<Profile> ListProfiles();
        //void DeleteUnAssignedTariffs();
    }
}
