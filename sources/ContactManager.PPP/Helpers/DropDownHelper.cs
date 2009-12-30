using System.Web.Mvc;
using ContactManager.PPP.Services;

namespace ContactManager.PPP.Helpers
{
    public class DropDownHelper
    {
        public SelectList GetPools(int? selectedValue)
        {
            var poolService = new PoolService(null);
            SelectList list;
            if (selectedValue.HasValue)
                list = new SelectList(poolService.ListPools(), "PoolId", "Name", selectedValue);
            else
                list = new SelectList(poolService.ListPools(), "PoolId", "Name");

            return list;
        }

        public SelectList GetProfiles(int? selectedValue)
        {
            var profileService = new ProfileService(null);
            if (selectedValue.HasValue)
                return new SelectList(profileService.ListProfiles(), "ProfileId", "DisplayName", selectedValue);
            return new SelectList(profileService.ListProfiles(), "ProfileId", "DisplayName");
        }
    }
}
