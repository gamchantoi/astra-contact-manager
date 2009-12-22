using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ContactManager.Models;
using ContactManager.Models.Validation;

namespace ContactManager.Messages.Models
{


    public class MessageTypeService
    {
        private readonly IValidationDictionary _validationDictionary;
        private readonly MessageTypeRepository _messageTypeRepository;
        public MessageTypeService(IValidationDictionary validationDictionary)
        {
            _validationDictionary = validationDictionary;
            _messageTypeRepository = new MessageTypeRepository();
        }

        public List<MessageType> ListMessagesTypes()
        {
            return _messageTypeRepository.ListMessageType();
        }

        public bool CreateMessageType(MessageType messageType)
        {
            try
            {
                _messageTypeRepository.CreateMessageType(messageType);
                return true;
            }
            catch (Exception ex)
            {
                _validationDictionary.AddError("_FORM", "Message Type not created. " + ex.Message);
                return false;
            }
        }

        public bool EditMessageType(MessageType messageType)
        {
            try
            {
                _messageTypeRepository.EditMessageType(messageType);
                return true;
            }
            catch (Exception ex)
            {
                _validationDictionary.AddError("_FORM", "Message Type not edited. " + ex.Message);
                return false;
            }
        }

        public MessageType GetMessageType(int id)
        {
            return _messageTypeRepository.GetMessageType(id);
        }


        public SelectList ListMessagesTypes(int? selectedValue)
        {
            var messageTypeService = new MessageTypeService(null);
            SelectList list;
            if (selectedValue.HasValue)
                list = new SelectList(messageTypeService.ListMessagesTypes(), "TypeId", "Name", selectedValue);
            else
                list = new SelectList(messageTypeService.ListMessagesTypes(), "TypeId", "Name");

            return list;
        }
    }
}
