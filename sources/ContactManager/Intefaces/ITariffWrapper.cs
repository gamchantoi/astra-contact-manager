using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.MobileControls;

namespace ContactManager.Intefaces
{
    public interface ITariffWrapper
    {
        String TargetAddresses { get; set; }
        String Comment { get; set; }
        String Status { get; set; }
        List<Guid> Clients { get; set; }
    }
}
