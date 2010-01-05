using System.Collections.Generic;
using ContactManager.Models;
using ContactManager.SSH.Intefaces;

namespace ContactManager.PPP.Intefaces
{
    public interface ISshPoolService
    {
        bool CreatePool(int id);
        bool EditPool(int id);
        bool DeletePool(int id);
        bool DeletePool(string name);
        List<Pool> ListPools();
        //ISSHRepository Repository { get; }
        //bool Connect();
        //void Disconnect();
    }
}