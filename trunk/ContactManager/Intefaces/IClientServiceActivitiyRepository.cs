using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ContactManager.Models;

namespace ContactManager.Intefaces
{
    public interface IClientServiceActivitiyRepository
    {
        ClientServiceActivitiy CreateActivity(ClientServiceActivitiy activity);
        ClientServiceActivitiy UpdateActivity(ClientServiceActivitiy activity);
    }
}
