using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ContactManager.Models;

namespace ContactManager.Intefaces
{
    public interface IPPPSecretRepository
    {
        PPPSecret CreatePPPSecret(PPPSecret secret);
        void DeletePPPSecret(Guid id);
        PPPSecret EditPPPSecret(PPPSecret secret);
        PPPSecret GetPPPSecret(Guid id);
        PPPSecret GetPPPSecret(string name);
        PPPSecret GetDefaultPPPSecret();
    }
}
