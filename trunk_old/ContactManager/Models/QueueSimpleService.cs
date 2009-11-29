using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContactManager.Intefaces;
using ContactManager.Models.Validation;

namespace ContactManager.Models
{
    public class QueueSimpleService : IQueueSimpleService
    {
        private IValidationDictionary _validationDictionary;
        private IQueueSimpleRepository _repository;
        private IContactService _contactService;

        #region Constructors
        public QueueSimpleService(IValidationDictionary validationDictionary)
            : this(validationDictionary, new EntityQueueSimpleRepository())
        { }

        public QueueSimpleService(IValidationDictionary validationDictionary, IQueueSimpleRepository repository)
        {
            _validationDictionary = validationDictionary;
            _repository = repository;
            _contactService = new ContactService(validationDictionary);
        }

        public QueueSimpleService(IValidationDictionary validationDictionary, 
            IQueueSimpleRepository repository, IContactService contactService)
        {
            _validationDictionary = validationDictionary;
            _repository = repository;
            _contactService = contactService;
        }
        #endregion

        public bool ValidateQueue(QueueSimple queue)
        {
            bool isValid = true;
            if (string.IsNullOrEmpty(queue.QueueName))
            {
                _validationDictionary.AddError("QueueName", "QueueName is required.");
                isValid = false;
            }
            return isValid;
        }

        #region IQueueSimpleService Members

        public bool CreateQueue(QueueSimple queue)
        {
            if (!ValidateQueue(queue))
                return false;
            try
            {
                _repository.CreateQueue(queue);
            }
            catch (Exception ex)
            {
                _validationDictionary.AddError("_FORM", "Queue is not saved. " + ex.Message);
                return false;
            }
            return true;
        }

        public bool CreateQueue(Tariff tariff)
        {
            return CreateQueue(BuildQueue(tariff));
        }

        public bool DeleteQueue(int id)
        {
            try
            {
                _repository.DeleteQueue(id);
            }
            catch (Exception ex)
            {
                _validationDictionary.AddError("_FORM", "Queue is not deleted. " + ex.Message);
                return false;
            }
            return true;
        }

        public bool EditQueue(QueueSimple queue)
        {
            if (!ValidateQueue(queue))
                return false;
            try
            {
                _repository.EditQueue(queue);
            }
            catch (Exception ex)
            {
                _validationDictionary.AddError("_FORM", "Queue is not saved. " + ex.Message);
                return false;
            }
            return true;
        }

        public QueueSimple GetQueue(int id)
        {
            return _repository.GetQueue(id);
        }

        public List<QueueSimple> ListQueues()
        {
            return _repository.ListQueues();
        }

        #endregion

        private QueueSimple BuildQueue(Tariff tariff)
        {
            var queue = new QueueSimple
            {
                QueueName = tariff.Name,
                Comment = tariff.Description,
                TargetAddress = tariff.TargetAddresses,
                TariffId = tariff.TariffId,
            };
            if(tariff.Clients != null)
                foreach (var userId in tariff.Clients) 
                { 
                    queue.astra_Clients.Add(_contactService.GetContact(userId));
                }
            return queue;
        }
    }
}
