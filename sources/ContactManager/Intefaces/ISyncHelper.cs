using System.Collections.Generic;
using ContactManager.Models;

namespace ContactManager.Intefaces
{
    interface ISyncHelper
    {
        bool HostToDB(out List<ValidatorStatus> statuses);
        bool DBToHost(out List<ValidatorStatus> statuses);
    }
}