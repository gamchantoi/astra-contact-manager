using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ContactManager.Models;
using System.Web.Mvc;

namespace ContactManager.Intefaces
{
    public interface IClientServiceActivitiyService
    {
        bool UpdateActivity(FormCollection collection, Guid UserId);
        bool CreateActivity(ClientServiceActivitiy activity);
        bool CreateActivity(Guid client, int service);
        bool UpdateActivity(ClientServiceActivitiy activity);
        bool DisableActivity(Guid client, int service);
    }
}
