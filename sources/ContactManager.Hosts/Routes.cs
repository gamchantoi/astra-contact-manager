using System.Web.Mvc;

namespace ContactManager.Hosts
{
    public class Routes : AreaRegistration
    {
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Hosts_Default",
                "Hosts/{controller}/{action}/{id}",
                new { controller = "Host", action = "Index", id = "" },
                new[] { "ContactManager.Hosts.Controllers" }
                );
        }
        public override string AreaName
        {
            get { return "Hosts"; }
        }

    }
}