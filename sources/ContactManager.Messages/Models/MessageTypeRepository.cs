using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContactManager.Models;

namespace ContactManager.Messages.Models
{
    public class MessageTypeRepository : RepositoryBase<MessageType>
    {
        public List<MessageType> ListMessageType()
        {
            return ObjectContext.MessageTypes.ToList();
        }

        public MessageType CreateMessageType(MessageType messageType)
        {
            ObjectContext.AddToMessageTypes(messageType);
            ObjectContext.SaveChanges();
            return messageType;
        }

        public MessageType EditMessageType(MessageType messageType)
        {
            var _messageType = ObjectContext.MessageTypes.FirstOrDefault(t => t.TypeId == messageType.TypeId);
            ObjectContext.ApplyPropertyChanges(_messageType.EntityKey.EntitySetName, messageType);
            ObjectContext.SaveChanges();
            return _messageType;
        }

        public MessageType GetMessageType(int id)
        {
            return ObjectContext.MessageTypes.FirstOrDefault(t => t.TypeId == id);
        }
    }
}
