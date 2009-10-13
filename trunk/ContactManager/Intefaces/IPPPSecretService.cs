using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ContactManager.Models;

namespace ContactManager.Intefaces
{
    public interface IPPPSecretService
    {
        bool CreatePPPSecret(PPPSecret secret);
        bool CreatePPPSecret(Client client);
        bool DeletePPPSecret(Guid id);
        bool EditPPPSecret(PPPSecret secret);
        bool EditPPPSecret(Client client);
        bool UpdatePPPSecretAddresses(PPPSecret secret);
        PPPSecret GetPPPSecret(Guid id);
    }
}
