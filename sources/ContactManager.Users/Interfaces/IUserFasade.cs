﻿using System;
using System.Collections.Generic;
using ContactManager.Models;
using ContactManager.PPP.Intefaces;
using ContactManager.Users.ViewModels;

namespace ContactManager.Users.Interfaces
{
    public interface IUserFasade
    {
        ILoadMoneyService LoadMoneyService { get; }
        IUserService UserService { get; }
        IClientService ClientService { get; }
        ISecretService SecretService { get; }
        IStatusService StatusService { get; }

        bool CreateContact(ClientViewModel viewModel);
        bool CreateContact(PPPSecret pppSecret);
        bool DeleteContact(Guid id);
        bool ActivateContact(Guid id);
        bool EditContact(ClientViewModel viewModel);
        bool EditContact(PPPSecret pppSecret);
        bool CanSynchronize(Guid id);
        Client GetContact(Guid id);

        List<Client> ListContacts();
        List<Client> ListContacts(string role);
        List<Client> ListContacts(bool deleted);
        List<Client> ListContacts(string role, bool deleted);
    }
}