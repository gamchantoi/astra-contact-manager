using System.Collections.Generic;
using ContactManager.Models;
using ContactManager.Models.Enums;

namespace ContactManager.Services.Interfaces
{
    public interface IServiceRepository
    {
        List<Service> ListServices(STATUSES? status);
        List<ClientInServices> ListActivities();

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