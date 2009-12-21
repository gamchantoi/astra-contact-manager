using System.Web.Mvc;

namespace ContactManager.Messages
{
    public class Routes : AreaRegistration
    {
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Messages_Default",
                "Messages/{controller}/{action}/{id}",
                new { controller = "Message", action = "Index", id = "" },
                new[] { "ContactManager.Messages.Controllers" }
                );
        }
        public override string AreaName
        {
            get { return "Messages"; }
        }

    }
}