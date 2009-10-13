using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ContactManager.Models;

namespace ContactManager.Intefaces
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
