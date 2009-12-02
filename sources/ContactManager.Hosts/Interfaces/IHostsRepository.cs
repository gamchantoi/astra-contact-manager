using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ContactManager.Models;

namespace ContactManager.Hosts.Interfaces
{
    interface IHostsRepository
    {
        Host CreateHost(Host host);
        void DeleteHost(int id);
        Host EditHost(Host host);
        Host GetHost(int id);
        IEnumerable<Host> ListHosts();
    }
}
