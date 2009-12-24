using System.Collections.Generic;
using ContactManager.Models;
using ContactManager.Models.Enums;

namespace ContactManager.Services.Interfaces
{
    public interface IServiceService
    {
        List<Service> ListServices(STATUSES? status);
        List<ClientInServices> ListActivities();

        bool CreateService(Service service);
        Service GetService(int id);        
        bool EditService(Service service);
        bool UpdateSystemService(PPPSecret secret);
        
    }
}