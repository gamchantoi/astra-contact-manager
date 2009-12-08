using System.Web.Mvc;

namespace ContactManager.Accounts
{
    public class Routes : AreaRegistration
    {
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Accounts_Default",
                "Accounts/{controller}/{action}/{id}",
                new { controller = "Transaction", action = "Index", id = "" },
                new[] { "ContactManager.Accounts.Controllers" }
                );
        }
        public override string AreaName
        {
            get { return "Accounts"; }
        }

    }
}