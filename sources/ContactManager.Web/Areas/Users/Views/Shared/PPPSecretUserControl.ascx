<%@ Import Namespace="ContactManager.Models.Enums" %>
<%@ Import Namespace="ContactManager.Users.Services" %>
<%@ Import Namespace="ContactManager.Users.ViewModels" %>
<%@ Import Namespace="ContactManager.Models" %>
<%@ Import Namespace="ContactManager.Web.Helpers" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ClientViewModel>" %>
<%if (!Model.Role.Equals(ROLES.admin.ToString()) && Model.PPPSecret != null)
  {%>
<fieldset class="fields">
    <legend><a href="#" onclick="ShowDialog('../GetPopupData');">PPP Secret</a></legend>
    <%=Html.BuildItem("Name: ", Model.PPPSecret.Name)%>
    <%=Html.BuildItem("Password: ", Model.PPPSecret.Password)%>
    <%=Html.BuildItem("Local Address: ", Model.PPPSecret.LocalAddress)%>
    <%=Html.BuildItem("Remote Address: ", Model.PPPSecret.RemoteAddress)%>
    <%=Html.BuildItem("MAC Address: ", Model.PPPSecret.MACAddress)%>
    <%=Html.BuildItem("DHCP Address: ", Model.PPPSecret.DHCPAddress)%>
    <%=Html.BuildItem("Comment: ", Model.PPPSecret.Comment)%>
    <%=Html.BuildItem("Routes: ", Model.PPPSecret.Routes)%>
</fieldset>
<%} %>
