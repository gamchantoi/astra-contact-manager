using System;
using System.Collections.Generic;
using AutoMapper;
using ContactManager.Models;
using ContactManager.Models.Enums;
using ContactManager.Models.Validation;
using System.Text.RegularExpressions;
using ContactManager.PPP.Intefaces;
using ContactManager.PPP.Services;
using ContactManager.Users.Interfaces;
using ContactManager.Users.Services;
using ContactManager.Users.ViewModels;

namespace ContactManager.Users.Services
{
    public class UserFasade : IUserFasade
    {
        private readonly IValidationDictionary _validationDictionary;
        private const string PATTERN = @"[\(*\)*\[*\]*]";

        #region Constructors

        public UserFasade(IValidationDictionary validationDictionary)
        {
            _validationDictionary = validationDictionary;
            UserService = new UserService(validationDictionary);
            ClientService = new ClientService(validationDictionary);
            SecretService = new SecretService(validationDictionary);
            LoadMoneyService = new LoadMoneyService(validationDictionary, this);
            StatusService = new StatusService(validationDictionary);
        }

        #endregion

        #region Validators
        public bool ValidateContact(ClientViewModel viewModel, bool creatingUser)
        {
            var isValid = true;
            if (creatingUser && !string.IsNullOrEmpty(viewModel.UserName))
            {
                var mUser = UserService.GetUser(viewModel.UserName);
                if (mUser != null)
                {
                    _validationDictionary.AddError("UserName", "UserName " + viewModel.UserName + " already exist.");
                    isValid = false;
                }
            }
            if (!ValidateField("UserName", viewModel.UserName, viewModel.UserName, true))
                isValid = false;
            if (!ValidateField("Password", viewModel.Password, viewModel.UserName, true))
                isValid = false;
            return isValid;
        }

        public bool ValidateContact(PPPSecret secret, bool creatingUser)
        {
            var model = new ClientViewModel
                            {
                                UserName = secret.Name,
                                Password = secret.Password
                            };
            return ValidateContact(model, creatingUser);
        }

        private bool ValidateField(string fieldName, string fieldValue, string userName, bool specialSymbols)
        {
            var isValid = true;
            var r = new Regex(PATTERN, RegexOptions.Compiled);
            if (string.IsNullOrEmpty(fieldValue) || fieldValue.Trim().Length == 0)
            {
                _validationDictionary.AddError(fieldName, string.Format("User {0}: {1} is required.", userName, fieldName));
                isValid = false;
            }
            if (!specialSymbols)
                if (string.IsNullOrEmpty(fieldValue) || r.Match(fieldValue).Success)
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
        public IStatusService StatusService { get; private set; }
        public ISecretService SecretService { get; private set; }

        public bool CreateContact(ClientViewModel viewModel)
        {
            // Validation logic
            if (!ValidateContact(viewModel, true))
                return false;

            // Database logic
            try
            {
                viewModel.UserId = UserService.CreateUser(BuildUser(viewModel)).UserId;
                ClientService.CreateClient(BuildClient(viewModel));
                if (viewModel.Role.Equals(ROLES.client.ToString()))
                    return SecretService.CreatePPPSecret(BuildSecret(viewModel));
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
            // Validation logic
            if (!ValidateContact(pppSecret, true))
                return false;

            // Database logic
            try
            {
                pppSecret.UserId = UserService.CreateUser(BuildUser(pppSecret)).UserId;
                ClientService.CreateClient(BuildClient(pppSecret));
                SecretService.CreatePPPSecret(pppSecret);
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

        public bool EditContact(ClientViewModel viewModel)
        {
            if (!ValidateContact(viewModel, false))
                return false;

            try
            {
                UserService.EditUser(BuildUser(viewModel));
                ClientService.EditClient(BuildClient(viewModel));
                if (viewModel.Role.Equals("client"))
                {
                    SecretService.EditPPPSecret(BuildSecret(viewModel));
                }
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
            if (!ValidateContact(pppSecret, false))
                return false;

            try
            {
                UserService.EditUser(BuildUser(pppSecret));

                var _client = BuildClient(pppSecret);
                if (_client.UserId.Equals(Guid.Empty))
                    ClientService.CreateClient(_client);
                else
                    ClientService.EditClient(_client);

                var _secret = BuildSecret(pppSecret);
                if (_secret.UserId.Equals(Guid.Empty))
                    SecretService.CreatePPPSecret(_secret);
                else
                    SecretService.EditPPPSecret(_secret);
            }
            catch (Exception ex)
            {
                _validationDictionary.AddError("_FORM", "Contact is not saved. " + ex.Message);
                return false;
            }
            return true;
        }

        #region Builders
        private PPPSecret BuildSecret(ClientViewModel viewModel)
        {
            var secret = SecretService.GetPPPSecret(viewModel.UserId)
                ?? new PPPSecret
                       {
                           //Client = ClientService.GetClient(viewModel.UserId),
                           Name = viewModel.UserName
                       };

            if (!viewModel.UserName.Equals(secret.Name))
            {
                secret.OldName = secret.Name;
                secret.Name = viewModel.UserName;
            }

            secret.Client = ClientService.GetClient(viewModel.UserId); ;
            secret.Profile = SecretService.ProfileService.GetProfile(viewModel.ProfileId);
            secret.Disabled = !StatusService.GetStatus(viewModel.StatusId).IsActive;
            secret.Password = viewModel.Password;
            secret.Comment = viewModel.Comment;
            return secret;
        }

        private PPPSecret BuildSecret(PPPSecret secret)
        {
            var _secret = SecretService.GetPPPSecret(secret.UserId) ?? new PPPSecret();
            _secret.CallerId = secret.CallerId;
            _secret.Comment = secret.Comment;
            _secret.Disabled = secret.Disabled;
            _secret.LimitBytesIn = secret.LimitBytesIn;
            _secret.LimitBytesOut = secret.LimitBytesOut;
            _secret.Name = secret.Name;
            _secret.Password = secret.Password;
            _secret.RemoteAddress = secret.RemoteAddress;
            _secret.Routes = secret.Routes;
            _secret.Service = secret.Service;
            _secret.Client = ClientService.GetClient(secret.UserId);
            _secret.Profile = SecretService.ProfileService.GetProfile(secret.Profile.Name);

            return _secret;
        }

        private Client BuildClient(ClientViewModel viewModel)
        {
            var ctx = new CurrentContext();
            var client = ClientService.GetClient(viewModel.UserId)
                ?? new Client
                       {
                           User = ctx.GetUser(viewModel.UserId)
                       };
            client.Credit = viewModel.Credit;
            client.Status = StatusService.GetStatus(viewModel.StatusId);
            return client;
        }

        private Client BuildClient(PPPSecret secret)
        {
            Status status;
            if (secret.Disabled.HasValue)
                status = StatusService.GetStatus(secret.Disabled.Value ? STATUSES.Inactive : STATUSES.Active);
            else
                status = StatusService.GetStatus(STATUSES.Active);

            var client = ClientService.GetClient(secret.UserId) ??
                new Client
                  {
                      User = UserService.GetUser(secret.Name),
                      Credit = decimal.Zero,

                  };
            client.Status = status;

            return client;
        }

        private static User BuildUser(ClientViewModel viewModel)
        {
            Mapper.CreateMap<ClientViewModel, User>();
            return Mapper.Map<ClientViewModel, User>(viewModel);
        }

        private User BuildUser(PPPSecret secret)
        {
            var user = UserService.GetUser(secret.Name) ?? new User();
            //user.UserId = secret.UserId;
            user.UserName = secret.Name;
            user.Password = secret.Password;
            user.Role = ROLES.client.ToString();
            return user;
        }
        #endregion

        public Client GetContact(Guid id)
        {
            var client = ClientService.GetClient(id);
            var mUser = UserService.GetUser(id);
            var secret = SecretService.GetPPPSecret(id);
            if (client != null)
            {
                client.Role = UserService.GetRoleForUser(mUser.UserName);
                client.Password = mUser.GetPassword();
                client.Email = mUser.Email;
                client.StatusReference.Load();
                if (secret != null)
                {
                    client.Comment = secret.Comment;
                    if (secret.Profile != null)
                        client.ProfileId = secret.Profile.ProfileId;
                }
            }
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
                client.LoadStatusReferences();
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