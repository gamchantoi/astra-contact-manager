using System.Collections.Generic;
using ContactManager.Models;

namespace ContactManager.PPP.Intefaces
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