using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContactManager.Models;
using ContactManager.Models.Enums;
using ContactManager.Models.Validation;
using ContactManager.PPP;
using ContactManager.Synchronization.Interfaces;
using ContactManager.Users.Interfaces;
using ContactManager.Users.Services;

namespace ContactManager.Synchronization.Services
{
    public class SynchronizationService : ISynchronizationService
    {
        private readonly IValidationDictionary _validationDictionary;
        private readonly IUserFacade _userFacade;
        private readonly PPPFactory _pppFactory;

        #region Constructors

        public SynchronizationService(IValidationDictionary validationDictionary)
        {
            _validationDictionary = validationDictionary;
            _userFacade = new UserFacade(validationDictionary);
            _pppFactory = new PPPFactory(validationDictionary) {SSHAutoMode = false};
        }

        #endregion

        #region ISynchronizationService Members

        public bool SyncToHost()
        {
            var status = HttpContext.Current.Application["SyncStatus"];
            if (status != null && status.ToString().Equals("Started"))
            {
                _validationDictionary.AddError("_FORM", "Currently Server is Synchronizing..");
                return false;
            }

            try
            {
                HttpContext.Current.Application.Add("SyncStatus", "Started");

                _pppFactory.SSHConnect();

                var sshSecrets = _pppFactory.SSHSecretsService.ListPPPSecrets();
                var sshProfiles = _pppFactory.SSHProfilesService.ListPPPProfiles();
                var sssPools = _pppFactory.SSHPoolsService.ListPools();
                //var dbUsers = _contactService.ListContacts();

                ProcessPools(sssPools);
                ProcessProfiles(sshProfiles);
                ProcessPPPSecrets(sshSecrets);
            }
            catch (Exception ex)
            {
                _validationDictionary.AddError("_FORM", "Server not synchronized. " + ex.Message);
            }
            finally
            {
                _pppFactory.SSHDisconnect();
                HttpContext.Current.Application.Remove("SyncStatus");
            }
            return true;
        }

        private void ProcessPPPSecrets(List<PPPSecret> sshSecrets)
        {
            //_sshSevice.CacheBegin();
            //var count = 0;
            var contacts = _userFacade.UserService.ListUsers(ROLES.client.ToString());
            foreach (var contact in contacts)
            {
                var _contact = _pppFactory.SecretService.GetPPPSecret(contact.UserId);
                if (_contact == null) continue;

                if (sshSecrets.Exists(c => c.Name == _contact.Name))
                    _pppFactory.SSHSecretsService.EditPPPSecret(_contact.UserId);
                else
                    _pppFactory.SSHSecretsService.CreatePPPSecret(_contact.UserId);
                //if (count == 100)
                //{
                //    _sshSevice.CacheCommit();
                //    count = 0;
                //}
                //count++;
            }
            ////_sshSevice.CacheCommit();
        }

        private void ProcessProfiles(List<Profile> sshProfiles)
        {
            foreach (var profile in _pppFactory.ProfilesService.ListProfiles())
            {
                var profile1 = profile;
                if (sshProfiles.Exists(p => p.Name == profile1.Name))
                    _pppFactory.SSHProfilesService.EditPPPProfile(profile.ProfileId);
                else
                    _pppFactory.SSHProfilesService.CreatePPPProfile(profile.ProfileId);
            }
        }

        private void ProcessPools(List<Pool> sshPools)
        {
            var aditionalPools = new List<Pool>();
            foreach (var pool in _pppFactory.PoolsService.ListPools())
            {
                var pool1 = pool;
                if (pool.NextPool.HasValue && !sshPools.Exists(p => p.Name == pool1.NextPoolName))
                {
                    aditionalPools.Add(pool);
                    continue;
                }
                if (sshPools.Exists(p => p.Name == pool1.Name))
                    _pppFactory.SSHPoolsService.EditPool(pool.PoolId);
                else
                    _pppFactory.SSHPoolsService.CreatePool(pool.PoolId);
            }

            foreach (var pool in aditionalPools)
            {
                var pool1 = pool;
                if (sshPools.Exists(p => p.Name == pool1.Name))
                    _pppFactory.SSHPoolsService.EditPool(pool.PoolId);
                else
                    _pppFactory.SSHPoolsService.CreatePool(pool.PoolId);
            }
        }

        public bool SyncFromHost()
        {
            var status = HttpContext.Current.Application["SyncStatus"];
            if (status != null && status.ToString().Equals("Started"))
            {
                _validationDictionary.AddError("_FORM", "Currently Server is Synchronizing..");
                return false;
            }

            try
            {
                HttpContext.Current.Application.Add("SyncStatus", "Started");
                _pppFactory.SSHAutoMode = false;

                _pppFactory.SSHConnect();
                var sshSecrets = _pppFactory.SSHSecretsService.ListPPPSecrets();
                var sshProfiles = _pppFactory.SSHProfilesService.ListPPPProfiles();
                var sshPools = _pppFactory.SSHPoolsService.ListPools();
                _pppFactory.SSHDisconnect();
  
                var dbUsers = _userFacade.UserService.ListUsers(ROLES.client.ToString());

                _pppFactory.PoolsService.CreateOrEditPools(sshPools);
                _pppFactory.ProfilesService.CreateOrEditProfiles(sshProfiles);

                foreach (var user in sshSecrets)
                {
                    var user1 = user;
                    var _user = dbUsers.Find(u => u.UserName.Equals(user1.Name.Trim(), StringComparison.OrdinalIgnoreCase));
                    if (_user != null)
                    {
                        user1.UserId = _user.UserId;
                        _userFacade.EditContact(user1);
                    }
                    else
                        _userFacade.CreateContact(user1);
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Application.Remove("SyncStatus");
                _validationDictionary.AddError("_FORM", "Server not synchronized. " + ex.Message);
            }
            finally
            {
                HttpContext.Current.Application.Remove("SyncStatus");
            }
            return true;
        }

        #endregion
    }
}