using System.Collections.Generic;
using ContactManager.PPP.Models;

namespace ContactManager.PPP.Intefaces
{
    interface IActiveConnectionsService
    {
        List<ActiveConnections> ListActiveConnections();
    }
}
