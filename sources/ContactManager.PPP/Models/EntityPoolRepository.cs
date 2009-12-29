using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContactManager.Models;
using ContactManager.PPP.Intefaces;

namespace ContactManager.PPP.Models
{
    public class EntityPoolRepository : RepositoryBase<Pool>, IPoolRepository
    {
        #region IPoolRepository Members

        public Pool CreatePool(Pool pool)
        {
            if (pool.NextPool.HasValue && pool.NextPool.Value != 0)
                pool.NextPool = ObjectContext.PoolSet.Where(p => p.PoolId == pool.NextPool.Value).FirstOrDefault().PoolId;            
            pool.LastUpdatedDate = DateTime.Now;
            ObjectContext.AddToPoolSet(pool);
            ObjectContext.SaveChanges();
            return pool;
        }

        public void DeletePool(int id)
        {
            var pool = ObjectContext.PoolSet.Where(p => p.PoolId == id).FirstOrDefault();
            var nextPoolCount = ObjectContext.PoolSet.Where(p => p.NextPool == pool.PoolId && p.PoolId != pool.PoolId).Count();

            if (nextPoolCount > 0)
                throw new Exception(string.Format("Pool '{0}' is used in {1} pool(s), and can't be deleted.",
                                                  pool.Name, nextPoolCount));
            
            pool.mkt_PPPProfiles.Load();
            if(pool.mkt_PPPProfiles.Count() > 0)
                throw new Exception(string.Format("Pool '{0}' is used by {1} Profile(s), and can't be deleted.",
                                                  pool.Name, pool.mkt_PPPProfiles.Count()));
            
            ObjectContext.DeleteObject(pool);
            ObjectContext.SaveChanges();
        }

        public Pool EditPool(Pool pool)
        {
            var _pool = ObjectContext.PoolSet.Where(p => p.PoolId == pool.PoolId).FirstOrDefault();

            if (String.IsNullOrEmpty(_pool.OldName) && (!_pool.Name.Equals(pool.Name)))
                pool.OldName = _pool.Name;

            ObjectContext.ApplyPropertyChanges(_pool.EntityKey.EntitySetName, pool);
            _pool.LastUpdatedDate = DateTime.Now;
            ObjectContext.SaveChanges();
            return _pool;
        }

        public Pool GetPool(int id)
        {
            return ObjectContext.PoolSet.Where(p => p.PoolId == id).FirstOrDefault();
        }

        public Pool GetPool(string name)
        {
            return ObjectContext.PoolSet.Where(p => p.Name == name).FirstOrDefault();
        }

        public List<Pool> ListPools()
        {
            foreach (var pool in ObjectContext.PoolSet.ToList())
            {
                if (!pool.NextPool.HasValue) continue;
                var _pool = ObjectContext.PoolSet.Where(p => p.PoolId == pool.NextPool);
                if (_pool.Count() > 0)
                    pool.NextPoolName = _pool.FirstOrDefault().Name;
            }
            return ObjectContext.PoolSet.ToList();
        }

        #endregion
    }
}