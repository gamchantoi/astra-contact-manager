using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ContactManager.Models;

namespace ContactManager.Intefaces
{
    public interface ISynchronizationService
    {
        bool SyncToHost();
        bool SyncFromHost();
    }
}
