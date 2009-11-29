using System.Collections.Generic;
using System.Web.Mvc;
using ContactManager.Models;

namespace ContactManager.Users.Interfaces
{
    public interface IStatusService
    {
        List<Status> ListStatuses();
        SelectList ListStatuses(int? selectedId);
        //bool CreateStatus(Status status);
        Status GetStatus(int id);
        Status GetStatus(Statuses status);
        bool EditStatus(Status status);
    }
}