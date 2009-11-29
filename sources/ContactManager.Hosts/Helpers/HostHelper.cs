using System.Linq;
using System.Web;
using ContactManager.Models;

namespace ContactManager.Hosts.Helpers
{
    public class HostHelper
    {
        public Host GetCurrentHost()
        {
            var entity = new AstraEntities();
            var hostId = int.Parse(HttpContext.Current.Profile.GetPropertyValue("HostId").ToString());
            return entity.HostSet.Where(h => h.HostId == hostId).FirstOrDefault();
        }
    }
}
