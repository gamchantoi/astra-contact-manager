﻿using System;
using System.Collections.Generic;
using ContactManager.Accounts.Interfaces;
using ContactManager.Accounts.Services;
using ContactManager.Models;
using ContactManager.Models.Validation;
using System.Web.Security;
using System.Text.RegularExpressions;
using ContactManager.PPP.Intefaces;
using ContactManager.PPP.Models;
using ContactManager.Users.Interfaces;
using ContactManager.Users.Services;

namespace ContactManager.Users.Services
{
    public class UserFasade : IUserFasade
    {
        private readonly IValidationDictionary _validationDictionary;
        private readonly ISecretService _pppSecretService;
        private const string PATTERN = @"[\(*\)*\[*\]*]";

        #region Constructors

        public UserFasade(IValidationDictionary validationDictionary)
        {
            //Entities = entities;
            _validationDictionary = validationDictionary;
            UserService = new UserService(validationDictionary);
            ClientService = new ClientService(validationDictionary);
            _pppSecretService = new SecretService(validationDictionary);
            LoadMoneyService = new LoadMoneyService(validationDictionary);
        }

        #endregion

        #region Validators
        public bool ValidateContact(Client client, bool creatingUser)
        {
            bool isValid = true;
            if (creatingUser)
            {
                var mUser = Membership.Provider.GetUser(client.UserName, false);
                if (mUser != null)
                {
                    _validationDictionary.AddError("_FORM", "UserName " + client.UserName + " already exist.");
                    isValid = false;
                }
            }
            if (!ValidateField("UserName", client.UserName, client.UserName, true))
                isValid = false;
            if (!ValidateField("Password", client.Password, client.UserName, true))
                isValid = false;
            return isValid;
        }

        private bool ValidateField(string fieldName, string fieldValue, string userName, bool specialSymbols)
        {
            var isValid = true;
            var r = new Regex(PATTERN, RegexOptions.Compiled);

            if (fieldValue.Trim().Length == 0)
            {
                _validationDictionary.AddError(fieldName, string.Format("User {0}: {1} is required.", userName, fieldName));
                isValid = false;
            }
            if (!specialSymbols)
                if (r.Match(fieldValue).Success)
                {
                    _validationDictionary.AddError(fieldName, string.Format("User {0}: {1} contain not allowed symbols '()[]'.", userName, fieldName));
                    isValid = false;
                }
            return isValid;
        }
        #endregion

        #region IContactService Members

        public ILoadMoneyService LoadMoneyService { get; private set; }
        public IUserService UserService { get; private set; }
        public IClientService ClientService { get; private set; }
        //public AstraObjectContext ObjectContext { get; private set; }

        public bool CreateContact(Client client)
        {
            // Validation logic
            if (!ValidateContact(client, true))
                return false;

            // Database logic
            try
            {
                UserService.CreateUser(client);
                ClientService.CreateClient(client);
                if (client.Role.Equals("client"))
                    _pppSecretService.CreatePPPSecret(client);
                return true;
            }
            catch (Exception ex)
            {
                //TODO: Roll Back created users
                _validationDictionary.AddError("_FORM", "Contact is not saved. " + ex.Message);
                return false;
            }
        }

        public bool CreateContact(PPPSecret pppSecret)
        {
            var client = ClientService.BuildClient(pppSecret);
            // Validation logic
            if (!ValidateContact(client, true))
                return false;

            // Database logic
            try
            {
                UserService.CreateUser(client);
                ClientService.CreateClient(client);
                pppSecret.UserId = client.UserId;
                _pppSecretService.CreatePPPSecret(pppSecret);
            }
            catch (Exception ex)
            {
                _validationDictionary.AddError("_FORM", "Contact is not saved. " + ex.Message);
                return false;
            }
            return true;
        }

        public bool DeleteContact(Guid id)
        {
            try
            {
                var ctx = new CurrentContext();
                if (id.Equals(ctx.CurrentUserId))
                    throw new Exception("Can't delete current user.");
                var client = ClientService.GetClient(id);

                //todo: disable status
                //client.Status = 0;
                ClientService.EditClient(client);
                //_pppSecretService.DeletePPPSecret(id);
                //_clientService.DeleteClient(id);
                //_accountService.DeleteUser(id);
            }
            catch (Exception ex)
            {
                _validationDictionary.AddError("_FORM", "Contact is not deleted. " + ex.Message);
                return false;
            }
            return true;
        }

        public bool ActivateContact(Guid id)
        {
            var client = ClientService.GetClient(id);
            //todo: enable status
            //client.Status = 1;
            ClientService.EditClient(client);
            return true;
        }

        public bool EditContact(Client client)
        {
            // Validation logic
            if (!ValidateContact(client, false))
                return false;

            // Database logic
            try
            {
                UserService.EditUser(client);
                ClientService.EditClient(client);
                if (client.Role.Equals("client"))
                {
                    //TODO: activate client if balanse is > 0
                    _pppSecretService.EditPPPSecret(client);
                }
                //_transactionService.CreateTransaction(client);
            }
            catch (Exception ex)
            {
                _validationDictionary.AddError("_FORM", "Contact is not saved. " + ex.Message);
                return false;
            }
            return true;
        }

        public bool EditContact(PPPSecret pppSecret)
        {
            var client = ClientService.BuildClient(pppSecret);
            client.UserId = new Guid(UserService.GetUser(client.UserName).ProviderUserKey.ToString());
            // Validation logic
            if (!ValidateContact(client, false))
                return false;

            // Database logic
            try
            {
                UserService.EditUser(client);
                //_clientService.EditClient(client);
                pppSecret.UserId = client.UserId;
                _pppSecretService.EditPPPSecret(pppSecret);
            }
            catch (Exception ex)
            {
                _validationDictionary.AddError("_FORM", "Contact is not saved. " + ex.Message);
                return false;
            }
            return true;
        }

        public Client GetContact(Guid id)
        {
            var client = ClientService.GetClient(id);
            var mUser = UserService.GetUser(id);
            client.Role = UserService.GetRoleForUser(mUser.UserName);
            client.Password = mUser.GetPassword();
            client.Email = mUser.Email;
            return client;
        }

        public string GetName(Guid id)
        {
            return UserService.GetUser(id).UserName;
        }

        public List<Client> ListContacts()
        {
            var clients = new List<Client>();
            foreach (var user in UserService.ListUsers())
            {
                user.astra_ClientsReference.Load();
                Client client;
                if (user.astra_ClientsReference.Value == null)
                    client = new Client
                                 {
                                     UserName = user.UserName
                                 };
                client = user.astra_ClientsReference.Value;
                client.Role = UserService.GetRoleForUser(user.UserName);
                client.UserName = user.UserName;
                client.LoadReferences();
                clients.Add(client);
            }
            return clients;
        }

        public List<Client> ListContacts(bool deleted)
        {
            var clients = ClientService.ListClients(deleted);
            foreach (var client in clients)
            {
                //todo: lazy load role
                client.Role = UserService.GetRoleForUser(client.UserName);
            }
        
            return clients;
        }

        public List<Client> ListContacts(string role, bool deleted)
        {
            var clients = new List<Client>();
            var status = deleted ? 1 : 0;
            foreach (var client in ListContacts(role))
            {
                //todo: status selector
                //if (client.Status == status) continue;
                //client.LoadReferences();
                clients.Add(client);
            }
            return clients;
        }

        public List<Client> ListContacts(string role)
        {
            var clients = new List<Client>();
            var users = UserService.ListUsers(role);
            foreach (var user in users)
            {
                user.astra_ClientsReference.Load();
                if (user.astra_ClientsReference.Value == null)
                    continue;
                user.astra_ClientsReference.Value.Role = role;
                user.astra_ClientsReference.Value.UserName = user.UserName;
                clients.Add(user.astra_ClientsReference.Value);
            }
            return clients;
        }

        public bool CanSynchronize(Guid id)
        {
            var user = UserService.GetUser(id);
            var role = UserService.GetRoleForUser(user.UserName);
            return role.Contains("client");
        }

        public bool DeleteAllData()
        {
            try
            {
                UserService.ClearAllData();
            }
            catch (Exception ex)
            {
                _validationDictionary.AddError("_FORM", "Database not cleaned. " + ex.Message + ex.InnerException.Message);
                return false;
            }
            return true;
        }

        #endregion


    }
}