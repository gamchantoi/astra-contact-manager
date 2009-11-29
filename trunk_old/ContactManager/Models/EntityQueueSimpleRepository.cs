using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContactManager.Intefaces;

namespace ContactManager.Models
{
    public class EntityQueueSimpleRepository : IQueueSimpleRepository
    {
        private AstraEntities _entities;

        #region Constructors
        public EntityQueueSimpleRepository()
        {
            _entities = new AstraEntities();
        }

        public EntityQueueSimpleRepository(AstraEntities entities)
        {
            _entities = entities;
        } 
        #endregion

        #region IQueueSimpleRepository Members

        public QueueSimple CreateQueue(QueueSimple queue)
        {
            queue.astra_Tariffs = _entities.TariffSet.Where(t => t.TariffId == queue.TariffId).FirstOrDefault();
            queue.LastUpdatedDate = DateTime.Now;
            _entities.AddToQueueSimpleSet(queue);
            _entities.SaveChanges();
            return queue;
        }

        public void DeleteQueue(int id)
        {
            var queue = _entities.QueueSimpleSet.Where(q => q.QueueId == id).FirstOrDefault();
            _entities.DeleteObject(queue);
            _entities.SaveChanges();
        }

        public QueueSimple EditQueue(QueueSimple queue)
        {
            var _queue = _entities.QueueSimpleSet.Where(q => q.QueueId == queue.QueueId).FirstOrDefault();
            _queue.astra_Tariffs = _entities.TariffSet.Where(t => t.TariffId == queue.TariffId).FirstOrDefault();            
            _entities.ApplyPropertyChanges(_queue.EntityKey.EntitySetName, queue);
            _queue.LastUpdatedDate = DateTime.Now;
            _entities.SaveChanges();
            return _queue;
        }

        public QueueSimple GetQueue(int id)
        {
            var queue = _entities.QueueSimpleSet.Where(q => q.QueueId == id).FirstOrDefault();
            queue.astra_Clients.Load();
            queue.astra_TariffsReference.Load();
            if (queue.astra_TariffsReference.Value != null)
                queue.TariffId = queue.astra_TariffsReference.Value.TariffId;
            return queue;
        }

        public List<QueueSimple> ListQueues()
        {
            return _entities.QueueSimpleSet.ToList();
        }

        #endregion
    }
}
