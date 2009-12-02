using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ContactManager.Models;

namespace ContactManager.Hosts.Interfaces
{
    interface IHostsService
    {
        bool CreateHost(Host host);
        bool DeleteHost(int id);
        bool EditHost(Host host);
        Host GetHost(int id);
        IEnumerable ListHosts();
    }
}
