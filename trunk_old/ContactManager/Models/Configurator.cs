using System;
using System.Linq;
using System.Web.Security;
using ContactManager.Models;

namespace ContactManager.Models
{
    public class Configurator
    {
        private readonly AstraEntities astra = new AstraEntities();

        public void ConfigureRoles()
        {
            if (Roles.RoleExists("admin")) return;
            
            Roles.CreateRole("admin");
            Roles.CreateRole("client");
            Roles.CreateRole("provider");
            //todo: Add user to roles
            //Roles.AddUserToRole(HttpContext.Current.User.Identity.Name, "admin");
        }

        public void ConfigureTariff() 
        {
            //var user = Membership.GetUser();
            //var userId = new Guid(user.ProviderUserKey.ToString());
            //var tariff = new Tariff { 
            //                            Direction = "both",
            //                            Name = "Default",
            //                            Priority = 8,
            //                            Profile = "default",
            //                            QueueType = "default/small-default/small",
            //                            Service = "any",
            //                            Limit_At = "0/0",
            //                            MaxDownloadLimit = 512000,
            //                            MaxUploadLimit = 1000000,                
            //                            LastUpdatedDate = DateTime.Now,
            //                            aspnet_Users = astra.ASPUserSet.Where(ui => ui.UserId == userId).First()
            //                        };
            //astra.AddToTariffSet(tariff);
            //astra.SaveChanges();
        }
        //todo: Create default admin user with config.
    }
}