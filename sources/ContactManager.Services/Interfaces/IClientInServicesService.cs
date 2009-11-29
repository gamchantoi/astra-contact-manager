using System;
using ContactManager.Models;
using System.Web.Mvc;

namespace ContactManager.Services.Interfaces
{
    public interface IClientInServicesService
    {
        bool UpdateActivity(FormCollection collection, Guid UserId);
        bool CreateActivity(ClientInServices activity);
        bool CreateActivity(Guid client, int service);
        bool UpdateActivity(ClientInServices activity);
        bool DisableActivity(Guid client, int service);
    }
}