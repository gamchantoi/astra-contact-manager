using System.Web.Mvc;

namespace ContactManager.Services
{
    public class Routes : AreaRegistration
    {
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Services_Default",
                "Services/{controller}/{action}/{id}",
                new { controller = "Service", action = "Index", id = "" },
                new[] { "ContactManager.Services.Controllers" }
                );
        }
        public override string AreaName
        {
            get { return "Services"; }
        }

    }
}