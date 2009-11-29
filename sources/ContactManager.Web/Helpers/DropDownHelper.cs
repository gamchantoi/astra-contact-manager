using System.Linq;
using System.Web.Mvc;
using ContactManager.Models;

namespace ContactManager.Web.Helpers
{
    public class DropDownHelper
    {
        public SelectList GetStatuses(string selectedValue)
        {
            if (string.IsNullOrEmpty(selectedValue))
                selectedValue = "Active";
            var list = new SelectList(new[] { "Active", "Disabled" }
                                          .Select(x => new { value = x, text = x }),
                                      "value", "text", selectedValue.Trim());
            return list;
        }

        //public SelectList GetAccountActivitiesMethods(int? selectedValue)
        //{
        //    AstraEntities _entities = new AstraEntities();
        //    SelectList list;
        //    if (selectedValue.HasValue)
        //        list = new SelectList(_entities.AccountTransactionMethodSet.Where(t => t.Visible).ToList(), "MethodId", "Name", selectedValue);
        //    else
        //        list = new SelectList(_entities.AccountTransactionMethodSet.Where(t => t.Visible).ToList(), "MethodId", "Name");

        //    return list;
        //}

        public SelectList GetStatuses(int selectedValue)
        {            
            return GetStatuses(selectedValue.Equals(1) ? "Active" : "Disabled");
        }

        //public SelectList GetRoles(string selectedValue)
        //{
        //    if (string.IsNullOrEmpty(selectedValue))
        //        selectedValue = "client";
        //    var helper = new UserHelper();
        //    var list = new SelectList(helper.GetRoles()
        //                                  .Select(x => new { value = x, text = x }),
        //                              "value", "text", selectedValue.Trim());
        //    return list;
        //}

        //public SelectList GetProfiles(int? selectedValue)
        //{
        //    var helper = new UserHelper();
        //    if (selectedValue.HasValue)
        //        return new SelectList(helper.GetProfiles(), "ProfileId", "Name", selectedValue);
        //    return new SelectList(helper.GetProfiles(), "ProfileId", "Name");
        //}

        //public SelectList GetPools(int? selectedValue)
        //{
        //    var helper = new UserHelper();
        //    SelectList list;            
        //    if (selectedValue.HasValue)
        //        list = new SelectList(helper.GetPools(), "PoolId", "Name", selectedValue);
        //    else
        //        list = new SelectList(helper.GetPools(), "PoolId", "Name");
            
        //    return list;
        //}
    }
}