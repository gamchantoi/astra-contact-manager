using System.Web.Mvc;

namespace ContactManager.PPP
{
    public class Routes : AreaRegistration
    {
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "PPP_Default",
                "PPP/{controller}/{action}/{id}",
                new { controller = "Profile", action = "Index", id = "" },
                new[] { "ContactManager.PPP.Controllers" }
                );
        }
        public override string AreaName
        {
            get { return "PPP"; }
        }

    }
}