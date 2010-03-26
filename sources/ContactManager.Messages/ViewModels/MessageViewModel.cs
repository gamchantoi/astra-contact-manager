using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ContactManager.Messages.ViewModels
{
    public class MessageViewModel
    {
        public IList<Messagess> Messagess { get; set; }
    }

    public class Messagess
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public int MessageId { get; set; }
    }
}
