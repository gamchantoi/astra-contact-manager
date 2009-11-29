using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContactManager.Intefaces;
using System.Web.Security;

namespace ContactManager.Models
{
    public class EntityContactRepository : IContactRepository
    {
        private AstraEntities _entities;

        #region Constructors
        public EntityContactRepository()
        {
            _entities = new AstraEntities();
        }

        public EntityContactRepository(AstraEntities entities)
        {
            _entities = entities;
        }
        #endregion

        #region IContactRepository Members

        public Client CreateContact(Client client)
        {
            var mUser = CreateUser(client.UserName, client.Password, client.Email, client.Role);
            client.UserId = new Guid(mUser.ProviderUserKey.ToString());
            var _client = CreateClient(client);
            CreatePPPSecret(_client);
            return _client;
        }

        private MembershipUser CreateUser(string name, string password, string email, string role)
        {
            MembershipCreateStatus mStatus;
            var mUser = Membership.Provider.CreateUser(name, password, email, null, null, true, null, out mStatus);
            if (mStatus == MembershipCreateStatus.Success)
            {
                Roles.AddUserToRole(name, role);
                return mUser;
            }
            return null;
        }

        public Client CreateContact(PPPSecret pppSecret)
        {
            var mUser = CreateUser(pppSecret.Name, pppSecret.Password, "", "client");
            var client = CreateClient(new Client
                        {
                            UserId = new Guid(mUser.ProviderUserKey.ToString())
                        });
            pppSecret.astra_Clients = client;
            _entities.AddToPPPSecretSet(pppSecret);
            _entities.SaveChanges();
            return CreateContact(client);
        }

        private Client CreateClient(Client client)
        {
            client.aspnet_Users = _entities.ASPUserSet.Where(u => u.UserId == client.UserId).FirstOrDefault();
            //client.mkt_PPPSecrets = CreatePPPSecret(client);
            client.LastUpdatedDate = DateTime.Now;
            _entities.AddToClientSet(client);
            _entities.SaveChanges();
            return client;
        }

        private PPPSecret CreatePPPSecret(Client client)
        {
            var pppSecret = new PPPSecret
            {
                UserId = client.UserId,
                Name = client.UserName,
                Password = client.Password,
                Status = client.Status.Equals("Active") ? 0 : 1,
                astra_Clients = client,
                mkt_PPPProfiles = _entities.ProfileSet.Where(p => p.ProfileId == client.ProfileId).FirstOrDefault(),
                LastUpdatedDate = DateTime.Now
            };
            _entities.AddToPPPSecretSet(pppSecret);
            _entities.SaveChanges();
            return pppSecret;
        }

        public void DeleteContact(Guid id)
        {
            var user = _entities.ASPUserSet.Where(u => u.UserId == id).FirstOrDefault();
            var client = _entities.ClientSet.Where(c => c.UserId == id).FirstOrDefault();
            if (client != null)
            {
                client.mkt_PPPSecretsReference.Load();
                if (client.mkt_PPPSecretsReference.Value != null)
                    _entities.DeleteObject(client.mkt_PPPSecretsReference.Value);
                _entities.DeleteObject(client);
                _entities.SaveChanges();
            }
            Membership.Provider.DeleteUser(user.UserName, true);
        }

        public Client EditContact(Client client)
        {
            var mUser = Membership.Provider.GetUser(client.UserId, true);

            if (!String.IsNullOrEmpty(client.Email)) mUser.Email = client.Email;

            if (!String.Equals(mUser.GetPassword(), client.Password))
                mUser.ChangePassword(mUser.GetPassword(), client.Password);
            Membership.Provider.UpdateUser(mUser);

            if (!String.IsNullOrEmpty(client.Role) && !Roles.IsUserInRole(mUser.UserName, client.Role))
            {
                Roles.RemoveUserFromRoles(mUser.UserName, Roles.GetRolesForUser(mUser.UserName));
                Roles.AddUserToRole(mUser.UserName, client.Role);
            }
            var _client = _entities.ClientSet.Where(c => c.UserId == client.UserId).FirstOrDefault();
            if (_client == null)
                return CreateClient(client);
            else
            {
                EditPPPSecret(client);
                _entities.ApplyPropertyChanges(_client.EntityKey.EntitySetName, client);
                _client.LastUpdatedDate = DateTime.Now;
                _entities.SaveChanges();
            }
            return _client;
        }

        private void EditPPPSecret(Client client)
        {
            var _client = _entities.ClientSet.Where(c => c.UserId == client.UserId).FirstOrDefault();
            _client.mkt_PPPSecretsReference.Load();
            var pppSecret = _client.mkt_PPPSecretsReference.Value;
            if (pppSecret == null)
                CreatePPPSecret(client);
            else
            {
                pppSecret.Password = client.Password;
                pppSecret.Status = client.Status.Equals("Active") ? 0 : 1;
                pppSecret.mkt_PPPProfiles = _entities.ProfileSet.Where(p => p.ProfileId == client.ProfileId).FirstOrDefault();
            }
        }

        public Client GetContact(Guid id)
        {
            var user = Membership.Provider.GetUser(id, false);
            var client = LoadDetails(id);
            client.UserName = user.UserName;
            client.Password = user.GetPassword();
            client.Role = Roles.GetRolesForUser(user.UserName)[0]; //take only first role
            client.Email = user.Email;
            return client;
        }

        public List<Client> ListContacts()
        {
            var users = new List<Client>();
            foreach (var user in _entities.ASPUserSet.ToList())
            {
                var role = String.Empty;
                foreach (var r in Roles.GetRolesForUser(user.UserName))
                {
                    role += r + " ";
                }
                //var mUser = Membership.Provider.GetUser(user.UserName, false);
                var u = LoadDetails(user.UserId);
                u.UserName = user.UserName;
                u.Role = role.Trim();
                //u.Password = mUser.GetPassword();

                users.Add(u);
            }
            return users;
        }

        public List<Client> ListContacts(string role)
        {
            var users = new List<Client>();
            var usersNames = Roles.GetUsersInRole(role);
            foreach (var user in usersNames)
            {
                var mUser = Membership.Provider.GetUser(user, false);
                var u = LoadDetails(new Guid(mUser.ProviderUserKey.ToString()));
                u.UserName = user;
                u.Role = role;
                u.Password = mUser.GetPassword();

                users.Add(u);
            }
            foreach (var user in _entities.ASPUserSet.ToList())
            {

            }
            return users;
        }

        public ASPUser CurrentUser()
        {
            var user = Membership.GetUser();
            var _currentUserId = new Guid(user.ProviderUserKey.ToString());
            return _entities.ASPUserSet.Where(ui => ui.UserId == _currentUserId).FirstOrDefault();
        }

        #endregion

        private Client LoadDetails(Guid id)
        {
            var client = _entities.ClientSet.Where(c => c.UserId == id).FirstOrDefault();
            if (client == null)
                return new Client { UserId = id };

            client.mkt_PPPSecretsReference.Load();
            var pppSecret = client.mkt_PPPSecretsReference.Value;
            if (pppSecret == null)
                return client;
            client.Status = pppSecret.Status.Value.Equals(0) ? "Active" : "Disabled";

            pppSecret.mkt_PPPProfilesReference.Load();
            var profile = pppSecret.mkt_PPPProfilesReference.Value;
            if (profile == null)
                return client;
            client.ProfileId = profile.ProfileId;
            client.ProfileName = profile.Name;

            //client.astra_ClientsDetailsReference.Load();
            //var details = client.astra_ClientsDetailsReference.Value;
            //if(details !=null)
            //    client.FullName = String.Format("{0} {1} {2}", details.FirstName, details.MiddleName, details.LastName);
            return client;
        }
    }
}
