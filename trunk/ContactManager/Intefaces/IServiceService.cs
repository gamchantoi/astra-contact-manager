using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ContactManager.Models;
using ContactManager.Helpers;

namespace ContactManager.Intefaces
{
    public interface IServiceService
    {
        List<Service> ListServices(Status? status);
        List<ClientServiceActivitiy> ListActivities();

        bool CreateService(Service service);
        Service GetService(int id);        
        bool EditService(Service service);

        bool UpdateSystemService(PPPSecret secret);
    }
}
