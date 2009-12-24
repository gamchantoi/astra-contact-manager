using System.Collections.Generic;
using ContactManager.Models;
using ContactManager.Models.Enums;

namespace ContactManager.Users.Interfaces
{
    public interface IStatusRepository
    {
        List<Status> ListStatuses();
        Status GetStatus(int id);
        Status GetStatus(STATUSES status);
        Status EditStatus(Status status);
    }
}