using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContactManager.Intefaces;
using ContactManager.Models.Validation;
using System.Web.Security;
using System.Text.RegularExpressions;

namespace ContactManager.Models
{
    public class ContactService : IContactService
    {
        private readonly IValidationDictionary _validationDictionary;
        private readonly IMembershipService _accountService;
        private readonly IClientService _clientService;
        private readonly IPPPSecretService _pppSecretService;
        private readonly IAccountTransactionService _transactionService;
        private Guid _currentClientId;
        private const string PATTERN = @"[\(*\)*\[*\]*]";

        #region Constructors
        public ContactService(IValidationDictionary validationDictionary)
            : this(validationDictionary, new AccountMembershipService()
            , new ClientService(validationDictionary), new PPPSecretService(validationDictionary)
            , new AccountTransactionService(validationDictionary))
        { }

        public ContactService(IValidationDictionary validationDictionary, IMembershipService accountService
            , IClientService clientService, IPPPSecretService pppSecretService
            , IAccountTransactionService transactionService)
        {
            _validationDictionary = validationDictionary;
            _accountService = accountService;
            _clientService = clientService;
            _pppSecretService = pppSecretService;
            _transactionService = transactionService;
        }
        #endregion

        public Guid CurrentClientId
        {
            get { return _currentClientId; }
        }

        public bool UserExist(string name)
        {
            return _accountService.UserExist(name);
        }

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

        #region IContactService Members

        public bool CreateContact(Client client)
        {
            // Validation logic
            if (!ValidateContact(client, true))
                return false;

            // Database logic
            try
            {
                _accountService.CreateUser(client);
                _clientService.CreateClient(client);
                if(client.Role.Equals("client"))
                    _pppSecretService.CreatePPPSecret(client);
                //_transactionService.CreateTransaction(client);
                return true;
            }
            catch (Exception ex)
            {
                //TODO Roll Back created users
                _validationDictionary.AddError("_FORM", "Contact is not saved. " + ex.Message);
                return false;
            }
        }

        public bool CreateContact(PPPSecret pppSecret)
        {
            var client = _clientService.BuildClient(pppSecret);
            // Validation logic
            if (!ValidateContact(client, true))
                return false;

            // Database logic
            try
            {
                _accountService.CreateUser(client);
                _clientService.CreateClient(client);
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
                if (id == new Guid(_accountService.GetCurrentUser().ProviderUserKey.ToString()))
                    throw new Exception("Can't delete current user.");
                var client = _clientService.GetClient(id);
                client.Status = 0;
                _clientService.EditClient(client);
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
            var client = _clientService.GetClient(id);
            client.Status = 1;
            _clientService.EditClient(client);
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
                _accountService.EditUser(client);
                _clientService.EditClient(client);
                if (client.Role.Equals("client"))
                    _pppSecretService.EditPPPSecret(client);
                _transactionService.CreateTransaction(client);
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
            var client = _clientService.BuildClient(pppSecret);
            client.UserId = new Guid(_accountService.GetUser(client.UserName).ProviderUserKey.ToString());
            // Validation logic
            if (!ValidateContact(client, false))
                return false;

            // Database logic
            try
            {
                _accountService.EditUser(client);
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
            var client = _clientService.GetClient(id);
            var mUser = _accountService.GetUser(id);
            client.Role = _accountService.GetRoleForUser(mUser.UserName);
            client.Password = mUser.GetPassword();
            client.Email = mUser.Email;
            client.UserName = mUser.UserName;
            client.LoadReferences();
            return client;
        }

        public string GetName(Guid id)
        {
            return _accountService.GetUser(id).UserName;
        }

        public List<Client> ListContacts() 
        {
            var clients = new List<Client>();
            foreach (var user in _accountService.ListUsers())
            {
                user.astra_ClientsReference.Load();
                Client client;
                if (user.astra_ClientsReference.Value == null)
                    client = new Client
                    {
                        UserName = user.UserName
                    };
                client = user.astra_ClientsReference.Value;
                client.Role = _accountService.GetRoleForUser(user.UserName);
                client.UserName = user.UserName;
                client.LoadReferences();
                clients.Add(client);
            }
            return clients;
        }
        
        public List<Client> ListContacts(bool deleted)
        {
            var clients = _clientService.ListClients(deleted);
            Client system = null;
            foreach (var client in clients)
            {
                client.aspnet_UsersReference.Load();
                var name = client.aspnet_UsersReference.Value.UserName;
                client.Role = _accountService.GetRoleForUser(name);
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
                if (client.Status == status) continue;
                //client.LoadReferences();
                clients.Add(client);
            }
            return clients;
        }

        public List<Client> ListContacts(string role)
        {
            var clients = new List<Client>();
            var users = _accountService.ListUsers(role);
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
            var user = _accountService.GetUser(id);
            var role = _accountService.GetRoleForUser(user.UserName);
            return role.Contains("client");
        }

        public bool DeleteAllData() 
        {
            try
            {
                _accountService.ClearAllData();
            }
            catch (Exception ex)
            {
                _validationDictionary.AddError("_FORM", "Database not cleaned. " + ex.Message + ex.InnerException.Message);
                return false;
            }
            return true;
        }

        public bool LoadMoney(Client client) 
        {
            try
            {
                _clientService.EditClient(client);
                _transactionService.CreateTransaction(client);
            }
            catch (Exception ex)
            {
                _validationDictionary.AddError("_FORM", "Money not loaded. " + ex.Message);
                return false;
            }
            return true;
        }

        public bool UpdateSecret(PPPSecret secret)
        {
            try
            {
                _pppSecretService.UpdatePPPSecretAddresses(secret);
                return true;
            }
            catch (Exception ex)
            {
                _validationDictionary.AddError("_FORM", "Contact is not saved. " + ex.Message);
                return false;
            }
        }

        #endregion
    }
}
