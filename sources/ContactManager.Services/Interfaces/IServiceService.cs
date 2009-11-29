using System.Collections.Generic;
using ContactManager.Models;

namespace ContactManager.Services.Interfaces
{
    public interface IServiceService
    {
        List<Service> ListServices(Statuses? status);
        List<ClientInServices> ListActivities();

        bool CreateService(Service service);
        Service GetService(int id);        
        bool EditService(Service service);

        bool UpdateSystemService(PPPSecret secret);
    }
}