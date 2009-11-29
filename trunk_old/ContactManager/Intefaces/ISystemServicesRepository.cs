using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ContactManager.Models;

namespace ContactManager.Intefaces
{
    public interface ISystemServicesRepository
    {
        SystemService GetService(string name);
        bool RegisterService(SystemService system, int serviceId);
    }
}
