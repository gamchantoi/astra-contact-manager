using System.Collections.Generic;
using System.Linq;

namespace ContactManager.Models
{
    public partial class Client
    {
        public string UserName { get; set; }
        public string SecretStatus { get; set; }
        public string Comment { get; set; }
        public string Role { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int ProfileId { get; set; }
        public int MethodId { get; set; }
        public decimal Load { get; set; }
        public string ProfileName { get; set; }

        public void LoadReferences()
        {
            if (EntityKey == null) return;

            mkt_PPPSecretsReference.Load();
            var pppSecret = mkt_PPPSecretsReference.Value;
            if (pppSecret != null)
            {
                Comment = pppSecret.Comment;
                SecretStatus = pppSecret.Status.Value.Equals(1) ? "Active" : "Disabled";

                pppSecret.mkt_PPPProfilesReference.Load();
                var profile = pppSecret.mkt_PPPProfilesReference.Value;
                if (profile != null)
                {
                    ProfileId = profile.ProfileId;
                    ProfileName = profile.Name;
                }
            }

            astra_ClientsDetailsReference.Load();
            var details = astra_ClientsDetailsReference.Value;
            if (details != null) 
            {
                FullName = string.Format("{0} {1} {2}", details.LastName, details.FirstName, details.MiddleName);
            }

            astra_ClientsServicesActivities.Load();
            foreach (var item in astra_ClientsServicesActivities)
            {
                if (item.Active == false) continue;

                item.astra_ServicesReference.Load();

                if (item.astra_ServicesReference.Value != null && 
                    item.astra_ServicesReference.Value.SystemData != null)
                {
                    if (item.astra_ServicesReference.Value.SystemData.Equals("Real_IP_Address"))
                    {
                        pppSecret.SystemRealIP = true;
                        continue;
                    }

                    if (item.astra_ServicesReference.Value.SystemData.Equals("Stay_OnLine"))
                    {
                        pppSecret.SystemStayOnline = true;
                        continue;
                    }
                }
            }
        }

        public void LoadDetailsReferences() 
        {
            aspnet_UsersReference.Load();
            UserName = aspnet_UsersReference.Value.UserName;
            //this.UserId = this.aspnet_UsersReference.Value.UserId;
            astra_AddressesReference.Load();
            astra_ClientsDetailsReference.Load();
            astra_ContractsReference.Load();
        }

        public List<ClientServiceActivitiy> LoadServicesActivities() 
        {
            astra_ClientsServicesActivities.Load();
            foreach (var item in astra_ClientsServicesActivities)
            {
                item.astra_ServicesReference.Load();
                item.ServiceId = item.astra_ServicesReference.Value.ServiceId;
            }
            return astra_ClientsServicesActivities.Where(a => a.Active).ToList();
        }
    }
}
