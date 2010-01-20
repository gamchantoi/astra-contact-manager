using System;
using System.Collections.Generic;
using System.Web.Security;
using ContactManager.Models;
using ContactManager.Models.Enums;
using ContactManager.Models.Validation;

namespace ContactManager.Messages.Models
{
    
    public class MessageService
    {
        private MessagesRepository _messagesRepository;
        private readonly IValidationDictionary _validationDictionary;

        public MessageService(IValidationDictionary validationDictionary)
        {
            _validationDictionary = validationDictionary;
            _messagesRepository = new MessagesRepository();
            MessageTypeService = new MessageTypeService(validationDictionary);
        }

        public MessageTypeService MessageTypeService { get; private set; }

        public List<Message> ListMessages()
        {
            CurrentContext ctx = new CurrentContext();
            
            if(Roles.IsUserInRole(ctx.CurrentASPUser.UserName, ROLES.admin.ToString()))
            return _messagesRepository.ListMessages();
            return _messagesRepository.ListMessages(ctx.CurrentASPUser.UserId);
        }

        public bool CreateMessage(Message message)
        {
            try
            {
                var ctx = new CurrentContext();
                var client = ctx.CurrentClient;
                var userId = ctx.CurrentUserId;

                var messageType = MessageTypeService.GetMessageType(message.MessageTypeId);
                message.MessageType = messageType;
                //message.MessageTypeId;
                var user = ctx.GetSystemUser();
                message.Client = client;
                message.User = user;
                //message.User.UserId = userId;
                _messagesRepository.CreateMessage(message);
                return true;
            }
            catch (Exception ex)
            {
                _validationDictionary.AddError("_FORM", "Message not created" + ex.Message);
                 return false;
            }
        }

        public Message GetMessage(int id)
        {
            var message = _messagesRepository.GetMessage(id);

            var ctx = new CurrentContext();
            var client = ctx.CurrentClient;
            var user = ctx.GetSystemUser();
            //var userId = ctx.CurrentUserId;

            //var messageType = MessageTypeService.GetMessageType(message.MessageTypeId);
            message.MessageTypeReference.Load();
            //message.MessageType = messageType;
            message.Client = client;
            message.User = user;
            
            return message;
        }

        public bool DeleteMessage(Message message)
        {
            try
            {
                _messagesRepository.DeleteMessage(message);
                return true;
            }
            catch(Exception ex)
            {
                _validationDictionary.AddError("_FORM","Message is not deleted"+ex.Message);
                return false;
            }
        }
    }
}
