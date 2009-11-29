using System;
using ContactManager.Models;

namespace ContactManager.PPP.Intefaces
{
    public interface ISecretRepository
    {
        AstraEntities Entities { get; }

        PPPSecret CreatePPPSecret(PPPSecret secret);
        void DeletePPPSecret(Guid id);
        PPPSecret EditPPPSecret(PPPSecret secret);
        PPPSecret GetPPPSecret(Guid id);
        PPPSecret GetPPPSecret(string name);
        PPPSecret GetDefaultPPPSecret();
    }
}