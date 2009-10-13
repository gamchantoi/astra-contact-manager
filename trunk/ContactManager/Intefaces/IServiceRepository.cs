using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ContactManager.Models;
using ContactManager.Helpers;

namespace ContactManager.Intefaces
{
    public interface IServiceRepository
    {
        List<Service> ListServices(Status? status);
        List<ClientServiceActivitiy> ListActivities();

        Service CreateService(Service service);
        Service GetService(int id);
        Service GetService(string name);
        Service BuildService(string systemData);
        Service EditService(Service service);

        bool ClientRegistered(Service service);
        bool RegisterClient(Service service);
        bool UnRegisterClient(Service service);
    }
}
