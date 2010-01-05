using System;
using System.Collections.Generic;
using ContactManager.Models;
using ContactManager.Models.Validation;
using ContactManager.PPP.Intefaces;
using ContactManager.PPP.Services;

namespace ContactManager.PPP.SSH
{
    public class SshProfileService : ISshProfileService
    {
        private readonly IValidationDictionary _validationDictionary;
        private readonly SshProfileRepository _repository;
        private readonly IProfileService _pppProfileService;

        public SshProfileService(IValidationDictionary validationDictionary) 
            : this(validationDictionary, true)
        {
        }

        public SshProfileService(IValidationDictionary validationDictionary, bool autoMode) 
        {
            _validationDictionary = validationDictionary;
            _repository = new SshProfileRepository(autoMode);
            _pppProfileService = new ProfileService(validationDictionary);
        }

        public bool CreatePPPProfile(int id)
        {
            try
            {
                var profile = _pppProfileService.GetProfile(id);
                _repository.ppp_profile_add(profile);
            }
            catch (Exception ex)
            {
                _validationDictionary.AddError("_FORM", "PPP Profile is not saved on host. " + ex.Message);
                return false;
            }
            return true;
        }

        public bool EditPPPProfile(int id)
        {
            try
            {
                var profile = _pppProfileService.GetProfile(id);
                _repository.ppp_profile_set(profile);
                //todo : resync update logic 
                profile.OldName = null;
                _pppProfileService.EditProfile(profile);
            }
            catch (Exception ex)
            {
                _validationDictionary.AddError("_FORM", "PPP Profile is not saved on host. " + ex.Message);
                return false;
            }
            return true;
        }

        public bool DeletePPPProfile(int id)
        {
            var profile = _pppProfileService.GetProfile(id);
            return DeletePPPProfile(profile.Name);
        }

        public bool DeletePPPProfile(string name)
        {
            try
            {
                _repository.ppp_profile_remove(name);
            }
            catch (Exception ex)
            {
                _validationDictionary.AddError("_FORM", name + " is not deleted on host. " + ex.Message);
                return false;
            }
            return true;
        }

        public List<Profile> ListPPPProfiles()
        {
            return _repository.ppp_profile_print();
        }

        //public bool Connect(Host host)
        //{
        //    return Connect(host.Address, host.UserName, host.UserPassword);
        //}
    }
}
