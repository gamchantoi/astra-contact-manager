using System.Globalization;
using System.Web.Compilation;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace ContactManager.Web.Helpers
{
    public static class HtmlExtensions
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

        public static string Resource(this HtmlHelper html, string expr, params object[] args)
        {
            var path = ((WebFormView)html.ViewContext.View).ViewPath;

            var fields = (ResourceExpressionFields)(new ResourceExpressionBuilder()).ParseExpression(
                expr,
                typeof(string),
                new ExpressionBuilderContext(path)
                );

            return (!string.IsNullOrEmpty(fields.ClassKey))
                ? string.Format((string)html.ViewContext.HttpContext.GetGlobalResourceObject(
                    fields.ClassKey,
                    fields.ResourceKey,
                    CultureInfo.CurrentUICulture),
                    args)

                : string.Format((string)html.ViewContext.HttpContext.GetLocalResourceObject(
                    path,
                    fields.ResourceKey,
                    CultureInfo.CurrentUICulture),
                    args);
        }

        public static string JSLink(this HtmlHelper html,string text, string function, string args)
        {
            var builder = new TagBuilder("a");
            builder.Attributes.Add("onclick", string.Format("{0}('{1}');", function, args));
            builder.Attributes.Add("href", "#");
            builder.SetInnerText(text);
            return builder.ToString();
        }
    }
}
