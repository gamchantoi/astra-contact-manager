using System.Collections.Generic;
using System.Web.Mvc;
using ContactManager.Models;
using ContactManager.Models.Enums;

namespace ContactManager.Users.Interfaces
{
    public interface IStatusService
    {
        List<Status> ListStatuses();
        SelectList ListStatuses(int? selectedId);
        Status GetStatus(int id);
        Status GetStatus(STATUSES status);
        bool EditStatus(Status status);
    }
}