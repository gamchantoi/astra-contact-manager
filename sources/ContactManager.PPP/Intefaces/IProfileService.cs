using System.Collections.Generic;
using ContactManager.Models;

namespace ContactManager.PPP.Intefaces
{
    public interface IProfileService
    {
        bool CreateProfile(Profile profile);
        bool CreateOrEditProfiles(List<Profile> profiles);
        bool DeleteProfile(int id);
        bool EditProfile(Profile profile);
        Profile GetProfile(int id);        
        List<Profile> ListProfiles();
    }
}