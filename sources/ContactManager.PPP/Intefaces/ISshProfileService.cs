using System.Collections.Generic;
using ContactManager.Models;
using ContactManager.SSH.Intefaces;

namespace ContactManager.PPP.Intefaces
{
    public interface ISshProfileService
    {
        bool CreatePPPProfile(int id);
        bool EditPPPProfile(int id);
        bool DeletePPPProfile(int id);
        bool DeletePPPProfile(string name);
        List<Profile> ListPPPProfiles();
        ISSHRepository Repository { get; }
        bool Connect(Host host);
        void Disconnect();
    }
}