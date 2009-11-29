using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ContactManager.Models;

namespace ContactManager.Intefaces
{
    public interface IProfileRepository
    {
        Profile CreateProfile(Profile profile);
        void DeleteProfile(int id);
        Profile EditProfile(Profile profile);
        Profile GetProfile(int id);
        Profile GetProfile(string name);
        //Profile GetProfile(string name);
        //Profile GetProfile(Profile profile);
        List<Profile> ListProfiles();
        //int DeleteUnAssignedProfiles();
    }
}
