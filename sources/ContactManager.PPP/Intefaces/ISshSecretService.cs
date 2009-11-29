using System;
using System.Collections.Generic;
using ContactManager.Models;

namespace ContactManager.PPP.Intefaces
{
    public interface ISshSecretService
    {
        bool CreatePPPSecret(Guid id);
        bool EditPPPSecret(Guid id);
        bool DeletePPPSecret(Guid id);
        bool DeletePPPSecret(string name);
        List<PPPSecret> ListPPPSecrets();
        bool Connect(Host host);
        void Disconnect();
    }
}