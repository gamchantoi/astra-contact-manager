<%@ Import Namespace="ContactManager.Models.Enums" %>
<%@ Import Namespace="ContactManager.Models" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="ContactManager.Web.Helpers" %>
<%
    if (Request.IsAuthenticated)
    {
%>
<% var ctx = new CurrentContext();%>
<% var host = ctx.GetCurrentHost();%>

<% if (host == null)
   {%>

<script type="text/javascript">
    $(document).ready(function() {
        ShowHostsDialog(' <%= Url.Content("~/Hosts/Host/Index")%> ');
    });
</script>

<%} %>
<%
    if (!Page.User.IsInRole(ROLES.client.ToString()))
    {  %>
<%= Html.Resource("Web_Resources, Site_LogOnUserControl_ActiveServer")%>
<%= Html.JSLink(host != null ? host.Address : Html.Resource("Web_Resources, Site_LogOnUserControl_NoServerSelected"), "ShowHostsDialog", Url.Content("~/Hosts/Host/Index"))%>
<%} %>
<%= Html.Resource("Web_Resources, Site_LogOnUserControl_Welcome") %>
<b>
    <%= Html.Encode(Page.User.Identity.Name) %></b>! [
<%= Html.ActionLink(Html.Resource("Web_Resources, Site_LogOnUserControl_LogOff"), "LogOff", "Account", new { area=""}, null)%>
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
