using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ContactManager.Models;

namespace ContactManager.Intefaces
{
    public interface IPoolService
    {
        bool CreatePool(Pool pool);
        bool CreateOrEditPools(List<Pool> pools);
        bool DeletePool(int id);
        bool EditPool(Pool pool);
        Pool GetPool(int id);
        Pool GetPool(string name);
        List<Pool> ListPools();
    }
}
