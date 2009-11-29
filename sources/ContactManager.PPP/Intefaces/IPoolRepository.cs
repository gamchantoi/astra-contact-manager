using System.Collections.Generic;
using ContactManager.Models;

namespace ContactManager.PPP.Intefaces
{
    public interface IPoolRepository
    {
        Pool CreatePool(Pool pool);        
        void DeletePool(int id);
        Pool EditPool(Pool pool);
        Pool GetPool(int id);
        Pool GetPool(string name);
        List<Pool> ListPools();
    }
}