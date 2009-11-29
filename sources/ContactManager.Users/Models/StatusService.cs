using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ContactManager.Models;
using ContactManager.Models.Validation;
using ContactManager.Users.Interfaces;

namespace ContactManager.Users.Models
{
    public class StatusService : IStatusService
    {
        private readonly IValidationDictionary _validationDictionary;
        private readonly IStatusRepository _repository;

        #region Constructors
        public StatusService(IValidationDictionary validationDictionary)
            : this(validationDictionary, new AstraEntities())
        { }

        public StatusService(IValidationDictionary validationDictionary, AstraEntities entities)
        {
            _validationDictionary = validationDictionary;
            _repository = new EntityStatusRepository(entities);
        }
        #endregion

        public List<Status> ListStatuses()
        {
            return _repository.ListStatuses();
        }

        public SelectList ListStatuses(int? selectedId)
        {
            if (selectedId.HasValue)
                return new SelectList(ListStatuses(), "StatusId", "DisplayName", selectedId.Value);
            return new SelectList(ListStatuses(), "StatusId", "DisplayName");
        }

        public Status GetStatus(int id)
        {
            return _repository.GetStatus(id);
        }

        public Status GetStatus(Statuses status)
        {
            return _repository.GetStatus(status);
        }

        public bool EditStatus(Status status)
        {
            try
            {
                _repository.EditStatus(status);
            }
            catch (Exception ex)
            {
                _validationDictionary.AddError("_FORM", "Status is not saved. " + ex.Message);
                return false;
            }
            return true;
        }
    }
}
