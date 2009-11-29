using ContactManager.Models;

namespace ContactManager.Services.Interfaces
{
    public interface ISystemServicesRepository
    {
        SystemService GetService(string name);
        bool RegisterService(SystemService system, int serviceId);
    }
}