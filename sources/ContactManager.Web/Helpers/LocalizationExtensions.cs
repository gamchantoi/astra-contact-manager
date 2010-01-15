using System.Globalization;
using System.Web.Compilation;
using System.Web.Mvc;

namespace ContactManager.Web.Helpers
{
    public static class LocalizationExtensions
    {

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
    }
}