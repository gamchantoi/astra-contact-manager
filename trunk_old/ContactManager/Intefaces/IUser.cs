using System;

namespace ContactManager.Intefaces
{
    public interface IUser
    {
        Guid UserId { get; set; }
        int TariffId { get; set; }
        int HostId { get; set; }
        String UserName { get; set; }
        String Password { get; set; }
        String Role { get; set; }
        String Email { get; set; }
        String Status { get; set; }
        Decimal Balance { get; set; }
        String FirstName { get; set; }
        String MiddleName { get; set; }
        String LastName { get; set; }
        String TariffName { get; set; }
        String LocalAddress { get; set; }
        String RemoteAddress { get; set; }
        void SetClientFields();
    }
}