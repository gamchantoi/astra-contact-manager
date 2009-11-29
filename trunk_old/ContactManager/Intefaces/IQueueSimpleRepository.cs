using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ContactManager.Models;

namespace ContactManager.Intefaces
{
    public interface IQueueSimpleRepository
    {
        QueueSimple CreateQueue(QueueSimple queue);
        void DeleteQueue(int id);
        QueueSimple EditQueue(QueueSimple queue);
        QueueSimple GetQueue(int id);
        List<QueueSimple> ListQueues();
    }
}
