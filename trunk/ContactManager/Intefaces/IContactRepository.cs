using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ContactManager.Models;

namespace ContactManager.Intefaces
{
    public interface IContactRepository
    {
        Client CreateContact(Client client);
        Client CreateContact(PPPSecret pppSecret);
        void DeleteContact(Guid id);
        Client EditContact(Client client);
        Client GetContact(Guid id);
        List<Client> ListContacts();
        List<Client> ListContacts(string role);
        ASPUser CurrentUser();
    }
}
