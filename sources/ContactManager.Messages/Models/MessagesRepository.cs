using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContactManager.Messages.Interfaces;
using ContactManager.Models;

namespace ContactManager.Messages.Models
{
    public class MessagesRepository:RepositoryBase<Message>, IMessagesRepository
    {
        public List<Message> ListMessages()
        {
            return ObjectContext.Messages.ToList();
        }

        public Message CreateMessage(Message message)
        {
            message.Date = DateTime.Now;
            message.StatusId = 0;

            ObjectContext.AddToMessages(message);
            ObjectContext.SaveChanges();
            return message;
        }



        public Message GetMessage(int id)
        {
            return ObjectContext.Messages.FirstOrDefault(m => m.MessageId == id);
        }

        public void DeleteMessage(Message message)
        {
            var _message = ObjectContext.Messages.FirstOrDefault(m => m.MessageId == message.MessageId);
            ObjectContext.DeleteObject(_message);
            ObjectContext.SaveChanges();
        }
    }
}
