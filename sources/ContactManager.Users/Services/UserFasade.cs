using System;
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
            : this(validationDictionary, new AstraEntities())
        { }

        public UserFasade(IValidationDictionary validationDictionary, AstraEntities entities)
        {
            Entities = entities;
            _validationDictionary = validationDictionary;
            MembershipService = new MembershipService(Entities);
            ClientService = new ClientService(validationDictionary, Entities);
            _pppSecretService = new SecretService(validationDictionary, Entities);
            LoadMoneyService = new LoadMoneyService(validationDictionary, Entities);
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
        public IMembershipService MembershipService { get; private set; }
        public IClientService ClientService { get; private set; }
        public AstraEntities Entities { get; private set; }

        public bool CreateContact(Client client)
        {
            // Validation logic
            if (!ValidateContact(client, true))
                return false;

            // Database logic
            try
            {
                MembershipService.CreateUser(client);
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
                MembershipService.CreateUser(client);
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
                if (id == new Guid(MembershipService.GetCurrentUser().ProviderUserKey.ToString()))
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
                MembershipService.EditUser(client);
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
            client.UserId = new Guid(MembershipService.GetUser(client.UserName).ProviderUserKey.ToString());
            // Validation logic
            if (!ValidateContact(client, false))
                return false;

            // Database logic
            try
            {
                MembershipService.EditUser(client);
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
            var mUser = MembershipService.GetUser(id);
            client.Role = MembershipService.GetRoleForUser(mUser.UserName);
            client.Password = mUser.GetPassword();
            client.Email = mUser.Email;
            client.UserName = mUser.UserName;
            client.LoadReferences();
            return client;
        }

        public string GetName(Guid id)
        {
            return MembershipService.GetUser(id).UserName;
        }

        public List<Client> ListContacts()
        {
            var clients = new List<Client>();
            foreach (var user in MembershipService.ListUsers())
            {
                user.astra_ClientsReference.Load();
                Client client;
                if (user.astra_ClientsReference.Value == null)
                    client = new Client
                                 {
                                     UserName = user.UserName
                                 };
                client = user.astra_ClientsReference.Value;
                client.Role = MembershipService.GetRoleForUser(user.UserName);
                client.UserName = user.UserName;
                client.LoadReferences();
                clients.Add(client);
            }
            return clients;
        }

        public List<Client> ListContacts(bool deleted)
        {
            var clients = ClientService.ListClients(deleted);
            Client system = null;
            foreach (var client in clients)
            {
                client.aspnet_UsersReference.Load();
                var name = client.aspnet_UsersReference.Value.UserName;
                client.Role = MembershipService.GetRoleForUser(name);
                client.UserName = name;
                client.LoadReferences();

                if (client.UserName.Equals("system", StringComparison.CurrentCultureIgnoreCase))
                {
                    system = client;
                }
            }
            if (system != null)
                clients.Remove(system);
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
            var users = MembershipService.ListUsers(role);
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
            var user = MembershipService.GetUser(id);
            var role = MembershipService.GetRoleForUser(user.UserName);
            return role.Contains("client");
        }

        public bool DeleteAllData()
        {
            try
            {
                MembershipService.ClearAllData();
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