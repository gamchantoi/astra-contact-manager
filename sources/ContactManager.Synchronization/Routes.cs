using System.Web.Mvc;

namespace ContactManager.Synchronization
{
    public class Routes : AreaRegistration
    {
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Synchronization_Default",
                "Synchronization/{controller}/{action}/{id}",
                new { controller = "User", action = "Index", id = "" },
                new[] { "ContactManager.Synchronization.Controllers" }
                );
        }
        public override string AreaName
        {
            get { return "Synchronizations"; }
        }

    }
}