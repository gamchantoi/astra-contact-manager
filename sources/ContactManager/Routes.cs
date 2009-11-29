using System.Web.Mvc;

namespace ContactManager
{
    public class Routes : AreaRegistration
    {
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                    "Main_Default",
                    "{controller}/{action}/{id}",
                    new { controller = "Home", action = "Index", id = "" },
                    new[] { "ContactManager.Controllers" }
                );
        }

        public override string AreaName
        {
            get { return "ContactManager"; }
        }
    }
}
