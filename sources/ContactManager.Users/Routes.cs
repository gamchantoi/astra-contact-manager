using System.Web.Mvc;

namespace ContactManager.Users
{
    public class Routes : AreaRegistration
    {
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Users_Default",
                "Users/{controller}/{action}/{id}",
                new { controller = "User", action = "Index", id = "" },
                new[] { "ContactManager.Users.Controllers" }
                );
        }
        public override string AreaName
        {
            get { return "Users"; }
        }

    }
}