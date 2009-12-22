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
                if(!UserReference.IsLoaded)
                    UserReference.Load();
                return User.UserName;

            } 
            set { }
        }

        public string SecretStatus { get; set; }
        public string Comment { get; set; }
        public string Role { get; set; }
        //public string FullName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int ProfileId { get; set; }
        public int MethodId { get; set; }
        public int StatusId { get; set; }
        public decimal Load { get; set; }
        //public string ProfileName { get; set; }

        public string GetProfileName()
        {
            if (EntityKey == null) return string.Empty;

            if(!PPPSecretReference.IsLoaded)
                PPPSecretReference.Load();

            if (PPPSecret != null && !PPPSecret.ProfileReference.IsLoaded)
                PPPSecret.ProfileReference.Load();

            if (PPPSecret == null || PPPSecret.Profile == null) return string.Empty;

            return PPPSecret.Profile.Name;
        }

        public string GetFullName()
        {
            if (EntityKey == null) return string.Empty;
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
                Comment = pppSecret.Comment;
                if (pppSecret.Disabled.HasValue)
                    SecretStatus = pppSecret.Disabled.Value ? "Active" : "Disabled";

                pppSecret.ProfileReference.Load();
                var profile = pppSecret.ProfileReference.Value;
                if (profile != null)
                {
                    ProfileId = profile.ProfileId;
                    //ProfileName = profile.Name;
                }
            }

            //astra_ClientsDetailsReference.Load();
            //var details = astra_ClientsDetailsReference.Value;
            //if (details != null)
            //{
            //    FullName = string.Format("{0} {1} {2}", details.LastName, details.FirstName, details.MiddleName);
            //}

            astra_ClientsInServices.Load();
            foreach (var item in astra_ClientsInServices)
            {
                item.astra_ServicesReference.Load();

                if (item.astra_ServicesReference.Value != null &&
                    item.astra_ServicesReference.Value.SystemData != null)
                {
                    if (item.astra_ServicesReference.Value.SystemData.Equals("Real_IP_Address"))
                    {
                        if (pppSecret != null) pppSecret.SystemRealIP = true;
                        continue;
                    }

                    if (item.astra_ServicesReference.Value.SystemData.Equals("Stay_OnLine"))
                    {
                        if (pppSecret != null) pppSecret.SystemStayOnline = true;
                        continue;
                    }
                }
            }
            StatusReference.Load();
            StatusId = StatusReference.Value.StatusId;
        }

        public void LoadDetailsReferences()
        {
            UserReference.Load();
            UserName = UserReference.Value.UserName;
            ClientDetailsReference.Load();
            //if (astra_ClientsDetailsReference.Value != null)
            //    FullName = string.Format("{0} {1} {2}", astra_ClientsDetailsReference.Value.LastName,
            //        astra_ClientsDetailsReference.Value.FirstName,
            //        astra_ClientsDetailsReference.Value.MiddleName);
            astra_AddressesReference.Load();
            astra_ContractsReference.Load();
        }

        public List<ClientInServices> LoadClientServices()
        {
            astra_ClientsInServices.Load();
            foreach (var item in astra_ClientsInServices)
            {
                item.astra_ServicesReference.Load();
                item.ServiceId = item.astra_ServicesReference.Value.ServiceId;
            }
            return astra_ClientsInServices.ToList();
        }
    }
}
