using ContactManager.Models;

namespace ContactManager.Services.Interfaces
{
    public interface ISystemServicesService
    {
        bool AddRealIp(int serviceId);
        bool AddStayOnline(int serviceId);
        bool RemoveRealIp(int serviceId);
        bool RemoveStayOnline(int serviceId);
        bool HasRealIp(Service service);
        bool HasStayOnline(Service service);
    }
}