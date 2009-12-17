using ContactManager.Models;

namespace ContactManager.Services.Interfaces
{
    public interface IClientInServicesRepository
    {
        ClientInServices CreateActivity(ClientInServices activity);
        ClientInServices UpdateActivity(ClientInServices activity);

        bool RemoveClientFromService(ClientInServices service);
    }
}