using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContactManager.PPP.Intefaces;

namespace ContactManager.Models
{
    public class EntityPoolRepository : IPoolRepository
    {
        private AstraEntities _entities;

        #region Constructors
        public EntityPoolRepository()
        {
            _entities = new AstraEntities();
        }

        public EntityPoolRepository(AstraEntities entities)
        {
            _entities = entities;
        }
        #endregion

        #region IPoolRepository Members

        public Pool CreatePool(Pool pool)
        {
            if (pool.NextPool.HasValue && pool.NextPool.Value != 0)
                pool.NextPool = _entities.PoolSet.Where(p => p.PoolId == pool.NextPool.Value).FirstOrDefault().PoolId;            
            pool.LastUpdatedDate = DateTime.Now;
            _entities.AddToPoolSet(pool);
            _entities.SaveChanges();
            return pool;
        }

        public void DeletePool(int id)
        {
            var pool = _entities.PoolSet.Where(p => p.PoolId == id).FirstOrDefault();
            var nextPoolCount = _entities.PoolSet.Where(p => p.NextPool == pool.PoolId && p.PoolId != pool.PoolId).Count();

            if (nextPoolCount > 0)
                throw new Exception(string.Format("Pool '{0}' is used in {1} pool(s), and can't be deleted.",
                    pool.Name, nextPoolCount));
            
            pool.mkt_PPPProfiles.Load();
            if(pool.mkt_PPPProfiles.Count() > 0)
                throw new Exception(string.Format("Pool '{0}' is used by {1} Profile(s), and can't be deleted.",
                    pool.Name, pool.mkt_PPPProfiles.Count()));
            
            _entities.DeleteObject(pool);
            _entities.SaveChanges();
        }

        public Pool EditPool(Pool pool)
        {
            var _pool = _entities.PoolSet.Where(p => p.PoolId == pool.PoolId).FirstOrDefault();
            _entities.ApplyPropertyChanges(_pool.EntityKey.EntitySetName, pool);
            _pool.LastUpdatedDate = DateTime.Now;
            _entities.SaveChanges();
            return _pool;
        }

        public Pool GetPool(int id)
        {
            return _entities.PoolSet.Where(p => p.PoolId == id).FirstOrDefault();
        }

        public Pool GetPool(string name)
        {
            return _entities.PoolSet.Where(p => p.Name == name).FirstOrDefault();
        }

        public List<Pool> ListPools()
        {
            foreach (var pool in _entities.PoolSet.ToList())
            {
                if (!pool.NextPool.HasValue) continue;
                var _pool = _entities.PoolSet.Where(p => p.PoolId == pool.NextPool);
                if (_pool.Count() > 0)
                    pool.NextPoolName = _pool.FirstOrDefault().Name;
            }
            return _entities.PoolSet.ToList();
        }

        #endregion
    }
}
