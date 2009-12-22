using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ContactManager.Models;

namespace ContactManager.Messages.Interfaces
{
    interface IMessagesRepository
    {
        List<Message> ListMessages();
    }
}
