using System.Linq;
using System.Web.Mvc;

namespace ContactManager.Models.Helpers
{
    public class StatusHelper
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
    }
}
