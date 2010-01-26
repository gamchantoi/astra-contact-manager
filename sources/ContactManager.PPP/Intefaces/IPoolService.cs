using System.Collections.Generic;
using System.Web.Mvc;
using ContactManager.Models;

namespace ContactManager.PPP.Intefaces
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
        SelectList ListPools(int? selectedValue);
    }
}