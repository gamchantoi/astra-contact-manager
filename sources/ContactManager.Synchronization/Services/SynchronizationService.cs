﻿using System;
using System.Collections.Generic;
using System.Web;
using ContactManager.Models;
using ContactManager.Models.Validation;
using ContactManager.PPP.Intefaces;
using ContactManager.PPP.Models;
using ContactManager.SSH.Models;
using ContactManager.Synchronization.Interfaces;

namespace ContactManager.Synchronization.Services
{
    public class SynchronizationService : SSHService, ISynchronizationService
    {
        private readonly IValidationDictionary _validationDictionary;
        //private readonly ISSHService _sshSevice;
        private AstraEntities _astraEntities;
        //private readonly IContactService _contactService;
        private readonly IPoolService _poolService;
        private readonly IProfileService _profileService;
        private readonly ISecretService _secretService;

        #region Constructors

        public SynchronizationService(IValidationDictionary validationDictionary)
            :base(validationDictionary, false)
        {
            //_astraEntities = entities;
            //_validationDictionary = validationDictionary;
            //_contactService = new ContactService(validationDictionary);
            //_poolService = new PoolService(validationDictionary, new EntityPoolRepository(entities));
            //_profileService = new ProfileService(validationDictionary, new EntityProfileRepository(entities));
            //_secretService = new SecretService(validationDictionary, new EntityPPPSecretRepository(entities));
            //_sshSevice = new SSHService(validationDictionary);
        }

        #endregion

        #region ISynchronizationService Members

        public bool SyncToHost()
        {
            //var status = HttpContext.Current.Application["SyncStatus"];
            //if (status != null && status.ToString().Equals("Started"))
            //{
            //    _validationDictionary.AddError("_FORM", "Currently Server is Synchronizing..");
            //    return false;
            //}

            //try
            //{
            //    HttpContext.Current.Application.Add("SyncStatus", "Started");

            //    var helper = new UserHelper();
            //    if (!_sshSevice.Connect(helper.GetCurrentHost())) return false;

            //    var sshSecrets = _sshSevice.ListPPPSecrets();
            //    var sshProfiles = _sshSevice.ListPPPProfiles();
            //    var sssPools = _sshSevice.ListPools();
            //    //var dbUsers = _contactService.ListContacts();

            //    ProcessPools(sssPools);
            //    ProcessProfiles(sshProfiles);
            //    ProcessPPPSecrets(sshSecrets);
            //}
            //catch (Exception)
            //{
            //    _sshSevice.Disconnect();
            //    return false;
            //}
            //finally
            //{
            //    HttpContext.Current.Application.Remove("SyncStatus");
            //}
            return true;
        }

        private void ProcessPPPSecrets(List<PPPSecret> sshSecrets)
        {
            //_sshSevice.CacheBegin();
            //int count = 0;
            //var contacts = _contactService.ListContacts("client", false);
            //foreach (var contact in contacts)
            //{
            //    var _contact = _secretService.GetPPPSecret(contact.UserId);
            //    if (_contact == null) continue;

            //    if (sshSecrets.Exists(c => c.Name == _contact.Name))
            //        _sshSevice.EditPPPSecret(_contact.UserId);
            //    else
            //        _sshSevice.CreatePPPSecret(_contact.UserId);
            //    //if (count == 100)
            //    //{
            //    //    _sshSevice.CacheCommit();
            //    //    count = 0;
            //    //}
            //    //count++;
            //}
            ////_sshSevice.CacheCommit();
        }

        private void ProcessProfiles(List<Profile> sshProfiles)
        {
            foreach (var profile in _profileService.ListProfiles())
            {
                //if (sshProfiles.Exists(p => p.Name == profile.Name))
                //    _sshSevice.EditPPPProfile(profile.ProfileId);
                //else
                //    _sshSevice.CreatePPPProfile(profile.ProfileId);
            }
        }

        private void ProcessPools(List<Pool> sssPools)
        {
            var aditionalPools = new List<Pool>();
            foreach (var pool in _poolService.ListPools())
            {
                if (pool.NextPool.HasValue && !sssPools.Exists(p => p.Name == pool.NextPoolName))
                {
                    aditionalPools.Add(pool);
                    continue;
                }
                //if (sssPools.Exists(p => p.Name == pool.Name))
                //    _sshSevice.EditPool(pool.PoolId);
                //else
                //    _sshSevice.CreatePool(pool.PoolId);
            }

            foreach (var pool in aditionalPools)
            {
                //if (sssPools.Exists(p => p.Name == pool.Name))
                //    _sshSevice.EditPool(pool.PoolId);
                //else
                //    _sshSevice.CreatePool(pool.PoolId);
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
                Connect();

                var sshSecrets = _sshSevice.ListPPPSecrets();
                //var sshProfiles = _sshSevice.ListPPPProfiles();
                //var sshPools = _sshSevice.ListPools();
                //_sshSevice.Disconnect();
                //var dbUsers = _contactService.ListContacts();

                //_poolService.CreateOrEditPools(sshPools);
                //_profileService.CreateOrEditProfiles(sshProfiles);

                //foreach (var user in sshSecrets)
                //{
                //    var user1 = user;
                //    if (dbUsers.Exists(u => u.UserName.Trim().Equals(user1.Name.Trim(), StringComparison.Ordinal)))
                //    {
                //        var _user = dbUsers.Where(u => u.UserName.Trim().Equals(user1.Name.Trim(), StringComparison.Ordinal)).FirstOrDefault();
                //        if (_user.Role == "admin") continue;
                //        if(_user.Status == 0)
                //            _contactService.ActivateContact(_user.UserId);
                //        _contactService.EditContact(user1);
                //    }
                //    else
                //        _contactService.CreateContact(user1);
                //}
            }
            catch (Exception)
            {

                HttpContext.Current.Application.Remove("SyncStatus");
                throw;
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