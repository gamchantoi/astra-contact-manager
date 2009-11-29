using System.Collections.Generic;
using ContactManager.Models;

namespace ContactManager.Users.Interfaces
{
    public interface IStatusRepository
    {
        List<Status> ListStatuses();
        //Status CreateStatus(Status status);
        Status GetStatus(int id);
        Status GetStatus(Statuses status);
        Status EditStatus(Status status);
    }
}