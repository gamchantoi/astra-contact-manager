using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Security;
using ContactManager.Models.Enums;

namespace ContactManager.Web.Helpers
{
    /// <summary>
    /// This helper method renders a link within an HTML LI tag.
    /// A class="selected" attribute is added to the tag when
    /// the link being rendered corresponds to the current
    /// controller and action.
    /// 
    /// This helper method is used in the Site.Master View 
    /// Master Page to display the website menu.
    /// </summary>
    public static class MenuItemHelper
    {
        public static string MenuItem(this HtmlHelper helper, string linkText, string actionName, string controllerName, string area)
        {
            if (Roles.IsUserInRole(ROLES.admin.ToString()) 
                || controllerName.Equals("Home")
                || controllerName.Equals("AccountTransactions")
                || controllerName.Equals("Message"))
            {

                var currentControllerName = (string)helper.ViewContext.RouteData.Values["controller"];
                var currentActionName = (string)helper.ViewContext.RouteData.Values["action"];

                var builder = new TagBuilder("li");

                builder.AddCssClass("ui-state-default ui-corner-top");

                // Add selected class
                if (currentControllerName.Equals(controllerName, StringComparison.CurrentCultureIgnoreCase))
                    builder.AddCssClass("ui-state-active");

                // Add link
                builder.InnerHtml = helper.ActionLink(linkText, actionName, controllerName, new { area = area }, null).ToHtmlString();

                // Render Tag Builder
                return builder.ToString(TagRenderMode.Normal);
            }
            return String.Empty;
        }
    }
}