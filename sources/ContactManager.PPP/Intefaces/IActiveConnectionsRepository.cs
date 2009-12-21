using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ContactManager.Models;
using ContactManager.PPP.Models;

namespace ContactManager.PPP.Intefaces
{
    interface IActiveConnectionsRepository
    {
        List<ActiveConnections> ppp_active_print();
    }
}
