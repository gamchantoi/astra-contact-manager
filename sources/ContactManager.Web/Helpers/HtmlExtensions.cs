﻿using System.Globalization;
using System.Text;
using System.Web.Compilation;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using ContactManager.Models;
using ContactManager.Models.Enums;

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

        public static string JSLink(this HtmlHelper html, string text, string function, string args)
        {
            var builder = new TagBuilder("a");
            builder.Attributes.Add("onclick", string.Format("{0}('{1}');", function, args));
            builder.Attributes.Add("href", "#");
            builder.SetInnerText(text);
            return builder.ToString();
        }

        public static string JSLink(this HtmlHelper html, string text, string function, string[] args)
        {
            var builder = new TagBuilder("a");
            var str = new StringBuilder();
            foreach (var s in args)
            {
                if (str.Length > 0)
                    str.Append(",");
                str.Append(string.Format("'{0}'", s));
            }

            builder.Attributes.Add("onclick", string.Format("{0}({1});", function, str));
            builder.Attributes.Add("href", "#");
            builder.SetInnerText(text);
            return builder.ToString();
        }

        public static string JSIconLink(this HtmlHelper html, string alt, string function, string args, string cssClass)
        {
            var builder = new TagBuilder("span");
            builder.AddCssClass("ui-icon-click ui-icon " + cssClass);
            if (function.Equals("window.location"))
                builder.Attributes.Add("onclick", string.Format("{0} = '{1}';", function, args));
            else
                builder.Attributes.Add("onclick", string.Format("{0}('{1}');", function, args));
            //builder.SetInnerText(text);
            return builder.ToString();
        }

        public static string SecureLink(this HtmlHelper html, string label, string link)
        {
            var ctx = new CurrentContext();
            var user = ctx.GetUser(ctx.CurrentUserId);

            return user.IsInRole(ROLES.admin) ? link : label;
        }

        public static string Message(this HtmlHelper html, string text, string elemetId)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            var builder = new TagBuilder("div");
            builder.AddCssClass("ui-widget ui-message");
            builder.Attributes.Add("id", elemetId);

            var div = new TagBuilder("div");
            var div2 = new TagBuilder("div");
            var p = new TagBuilder("p");

            var info = new TagBuilder("span");
            var close = new TagBuilder("span");
            close.Attributes.Add("onclick", "javascript:$('#" + elemetId + "').slideUp();");

            info.AddCssClass("ui-icon ui-icon-info");
            div.AddCssClass("ui-state-highlight ui-corner-all");
            div2.AddCssClass("container");
            close.AddCssClass("ui-icon-click ui-icon ui-icon-close");

            p.InnerHtml = info + text;
            div2.InnerHtml = p.ToString();
            div.InnerHtml = div2.ToString() + close;
            builder.InnerHtml = div.ToString();
            return builder.ToString();
        }
    }
}
