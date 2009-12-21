using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace ContactManager.Users.Helpers
{
    public class DropDownHelper
    {
        public SelectList GetRoles(string selectedValue)
        {
            if (string.IsNullOrEmpty(selectedValue))
                selectedValue = "client";
            var list = new SelectList(Roles.GetAllRoles()
                                          .Select(x => new { value = x, text = x }),
                                      "value", "text", selectedValue.Trim());
            return list;
        }
    }
}