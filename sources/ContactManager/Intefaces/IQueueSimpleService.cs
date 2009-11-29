using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ContactManager.Models;

namespace ContactManager.Intefaces
{
    public interface IQueueSimpleService
    {
        bool CreateQueue(QueueSimple queue);
        bool CreateQueue(Tariff tariff);
        bool DeleteQueue(int id);
        bool EditQueue(QueueSimple queue);
        QueueSimple GetQueue(int id);
        List<QueueSimple> ListQueues();
    }
}
