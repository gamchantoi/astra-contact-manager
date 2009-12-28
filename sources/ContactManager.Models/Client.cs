using System.Collections.Generic;
using System.Linq;

namespace ContactManager.Models
{
    public partial class Client
    {
        public string UserName
        {
            get
            {
                if (EntityKey == null) return string.Empty;
                if (!UserReference.IsLoaded)
                    UserReference.Load();
                return User.UserName;

            }
            set { }
        }

        public string Comment { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int ProfileId { get; set; }
        public decimal Load { get; set; }


        public List<Service> Services { get; set; }

        public string GetProfileName()
        {
            if (EntityKey == null) return string.Empty;

            if (!PPPSecretReference.IsLoaded)
                PPPSecretReference.Load();

            if (PPPSecret != null && !PPPSecret.ProfileReference.IsLoaded)
                PPPSecret.ProfileReference.Load();

            if (PPPSecret == null || PPPSecret.Profile == null) return string.Empty;

            return PPPSecret.Profile.Name;
        }

        public string GetFullName()
        {
            if (EntityKey == null) return string.Empty;
            if (!ClientDetailsReference.IsLoaded)
                ClientDetailsReference.Load();

            var details = ClientDetailsReference.Value;
            return details != null
                ? string.Format("{0} {1} {2}", details.LastName, details.FirstName, details.MiddleName)
                : string.Empty;
        }

        public void LoadReferences()
        {
            if (EntityKey == null) return;

            PPPSecretReference.Load();
            var pppSecret = PPPSecretReference.Value;
            if (pppSecret != null)
            {
                pppSecret.ProfileReference.Load();
                var profile = pppSecret.ProfileReference.Value;
                if (profile != null)
                {
                    ProfileId = profile.ProfileId;
                    //ProfileName = profile.Name;
                }
            }
        }

        public void LoadDetailsReferences()
        {
            if (!ClientDetailsReference.IsLoaded)
                ClientDetailsReference.Load();
        }

        public void LoadAddressReferences()
        {
            if (!AddressReference.IsLoaded)
                AddressReference.Load();

            if (Address != null)
                Address.StreetReference.Load();
        }

        public void LoadContractReferences()
        {
            if (!ContractReference.IsLoaded)
                ContractReference.Load();
        }

        public void LoadStatusReferences()
        {
            if (!StatusReference.IsLoaded)
                StatusReference.Load();
        }

        public List<ClientInServices> LoadClientServices()
        {
            astra_ClientsInServices.Load();
            if (Services == null) Services = new List<Service>();
            foreach (var item in astra_ClientsInServices)
            {
                item.astra_ServicesReference.Load();
                item.ServiceId = item.astra_ServicesReference.Value.ServiceId;
                Services.Add(item.astra_Services);
            }
            return astra_ClientsInServices.ToList();
        }
    }
}
