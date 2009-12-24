using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace ContactManager.Web.Helpers
{
    public static class DetailsHelper
    {
        public static string BuildItem(this HtmlHelper helper, string label, string value)
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;

            var builder = new TagBuilder("p")
                              {
                                  InnerHtml = (helper.Label(label).ToHtmlString() + helper.Encode(value))
                              };

            return builder.ToString();
        }
    }
}
