using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using ContactManager.Helpers;

namespace ContactManager.Helpers
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
        public static string MenuItem(this HtmlHelper helper, string linkText, string actionName, string controllerName)
        {
            var uHelper = new UserHelper();
            if (uHelper.IsUserInRole("admin") 
                || controllerName.Equals("Home")
                || controllerName.Equals("AccountTransactions"))
            {

                var currentControllerName = (string)helper.ViewContext.RouteData.Values["controller"];
                var currentActionName = (string)helper.ViewContext.RouteData.Values["action"];

                var builder = new TagBuilder("li");

                // Add selected class
                if (currentControllerName.Equals(controllerName, StringComparison.CurrentCultureIgnoreCase))
                    builder.AddCssClass("selected");

                // Add link
                builder.InnerHtml = helper.ActionLink(linkText, actionName, controllerName);

                // Render Tag Builder
                return builder.ToString(TagRenderMode.Normal);
            }
            return String.Empty;
        }
    }
}   