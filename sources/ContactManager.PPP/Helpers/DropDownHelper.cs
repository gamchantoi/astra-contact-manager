using System.Web.Mvc;
using ContactManager.PPP.Services;

namespace ContactManager.PPP.Helpers
{
    public class DropDownHelper
    {
        public SelectList GetProfiles(int? selectedValue)
        {
            var profileService = new ProfileService(null);
            if (selectedValue.HasValue)
                return new SelectList(profileService.ListProfiles(), "ProfileId", "DisplayName", selectedValue);
            return new SelectList(profileService.ListProfiles(), "ProfileId", "DisplayName");
        }
    }
}
