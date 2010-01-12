<%@ Import Namespace="ContactManager.Models" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="ContactManager.Web.Helpers" %>
<%
    if (Request.IsAuthenticated)
    {
%>
<%= Html.Resource("Web_Resources, Site_LogOnUserControl_ActiveServer")%>
<% var ctx = new CurrentContext();%>
<% var host = ctx.GetCurrentHost();%>
<%= Html.ActionLink(host != null ? host.Address : Html.Resource("Web_Resources, Site_LogOnUserControl_NoServerSelected"), "Index", "Host", new { area = "Hosts" }, null)%>
<%= Html.Resource("Web_Resources, Site_LogOnUserControl_Welcome") %>
<b>
    <%= Html.Encode(Page.User.Identity.Name) %></b>! [
<%= Html.ActionLink(Html.Resource("Web_Resources, Site_LogOnUserControl_LogOff"), "LogOff", "Account", null, null)%>
]
<%
    }
    else
    {
%>
[
<%= Html.ActionLink(Html.Resource("Web_Resources, Site_LogOnUserControl_LogOn"), "LogOn", "Account")%>
]
<%
    }
%>
