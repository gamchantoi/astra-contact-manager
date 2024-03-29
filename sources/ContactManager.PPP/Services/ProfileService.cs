﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ContactManager.Models;
using ContactManager.Models.Validation;
using System.Text.RegularExpressions;
using ContactManager.PPP.Intefaces;
using ContactManager.PPP.Models;
using ContactManager.PPP.Services;

namespace ContactManager.PPP.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IValidationDictionary _validationDictionary;
        private readonly IProfileRepository _repository;
        private readonly IPoolService _poolService;
        private const string PATTERN = @"[\(*\)*\[*\]*]";

        #region Constructors
        public ProfileService(IValidationDictionary validationDictionary)
        {
            _validationDictionary = validationDictionary;
            _repository = new EntityProfileRepository();
            _poolService = new PoolService(validationDictionary);
        } 
        #endregion

        public bool ValidateProfile(Profile profile)
        { 
            bool isValid = true;
            var r = new Regex(PATTERN, RegexOptions.Compiled);

            if (string.IsNullOrEmpty(profile.Name))
            {
                _validationDictionary.AddError("Name", "Profile Name is required.");
                isValid = false;
            }
            if (r.Match(profile.Name).Success)
            {
                _validationDictionary.AddError("Name", "Profile Name " + profile.Name + " contain not allowed symbols '()[]'.");
                isValid = false;
            }
            return isValid;
        }

        #region IProfileService Members

        public bool CreateProfile(Profile profile)
        {
            // Validation logic
            if (!ValidateProfile(profile))
                return false;

            // Database logic
            try
            {
                _repository.CreateProfile(profile);
            }
            catch (Exception ex)
            {
                _validationDictionary.AddError("_FORM", "Profile is not saved. " + ex.Message);
                return false;
            }
            return true;
        }

        public bool CreateOrEditProfiles(List<Profile> profiles)
        {
            foreach (var profile in profiles)
            {
                var p = _repository.GetProfile(profile.Name);
                var pool = _poolService.GetPool(profile.PoolName);

                if (pool != null)
                    profile.PoolId = pool.PoolId;

                if (string.IsNullOrEmpty(profile.DisplayName))
                    profile.DisplayName = profile.Name;

                if (p == null)
                {
                    CreateProfile(profile);
                    continue;
                }
                profile.ProfileId = p.ProfileId;                
                EditProfile(profile);
            }
            return true;
        }

        public bool DeleteProfile(int id)
        {
            try
            {
                var profile = GetProfile(id);
                if (profile.Name.Equals("default") ||
                    profile.Name.Equals("default-encryption"))
                    throw new Exception("Can't delete '" + profile.Name + "' profile");
                _repository.DeleteProfile(id);
            }
            catch (Exception ex)
            {
                _validationDictionary.AddError("_FORM", "Profile is not deleted. " + ex.Message);
                return false;
            }
            return true;
        }

        public bool EditProfile(Profile profile)
        {
            if (!ValidateProfile(profile))
                return false;
            try
            {
                _repository.EditProfile(profile);
            }
            catch (Exception ex)
            {
                _validationDictionary.AddError("_FORM", "Profile is not saved. " + ex.Message);
                return false;
            }
            return true;
        }

        public Profile GetProfile(int id)
        {
            return _repository.GetProfile(id);
        }

        public Profile GetProfile(string name)
        {
            return _repository.GetProfile(name);
        }

        public List<Profile> ListProfiles()
        {
            return _repository.ListProfiles();
        }

        public SelectList SelectListProfiles(int? selectedValue)
        {
            if (selectedValue.HasValue)
                return new SelectList(ListProfiles(), "ProfileId", "DisplayName", selectedValue);
            return new SelectList(ListProfiles(), "ProfileId", "DisplayName");
        }

        #endregion
    }
}