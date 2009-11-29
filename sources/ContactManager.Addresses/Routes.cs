using System.Web.Mvc;

namespace ContactManager.Addresses
{
    public class Routes : AreaRegistration
    {
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Addresses_Default",
                "Addresses/{controller}/{action}/{id}",
                new { controller = "Address", action = "Index", id = "" },
                new[] { "ContactManager.Addresses.Controllers" }
                );
        }
        public override string AreaName
        {
            get { return "Addresses"; }
        }

    }
}