using System;
using ContactManager.Models;

namespace ContactManager.PPP.Intefaces
{
    public interface ISecretService
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