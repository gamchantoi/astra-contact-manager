using System;
using ContactManager.Models;
using System.Web.Mvc;
using ContactManager.Services.ViewModels;

namespace ContactManager.Services.Interfaces
{
    public interface IClientInServicesService
    {
        //bool UpdateActivity(FormCollection collection, Guid UserId);
        bool CreateActivity(ClientInServices activity);
        //bool CreateActivity(Guid client, int service);
        bool AddClientToService(Guid clientId, int serviceId);
        bool UpdateActivity(ClientInServices activity);
        //bool RemoveClientFromService(Guid clientId, int serviceId);
        //bool DisableActivity(Guid client, int service);
        bool UpdateClientServices(FormCollection collection, Guid UserId);
        ClientServicesViewModel GetClientServices(Guid id);
    }
}