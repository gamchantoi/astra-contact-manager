using System;
using System.Collections.Generic;
using ContactManager.Models;

namespace ContactManager.Users.Interfaces
{
    public interface IUserFasade
    {
        ILoadMoneyService LoadMoneyService { get; }
        IMembershipService MembershipService { get; }
        IClientService ClientService { get; }
        AstraEntities Entities { get; }

        bool CreateContact(Client client);
        bool CreateContact(PPPSecret pppSecret);
        bool DeleteContact(Guid id);
        bool ActivateContact(Guid id);
        bool EditContact(Client client);
        bool EditContact(PPPSecret pppSecret);
        bool CanSynchronize(Guid id);
        string GetName(Guid id);
        Client GetContact(Guid id);

        List<Client> ListContacts();
        List<Client> ListContacts(string role);
        List<Client> ListContacts(bool deleted);
        List<Client> ListContacts(string role, bool deleted);

        bool DeleteAllData();
    }
}