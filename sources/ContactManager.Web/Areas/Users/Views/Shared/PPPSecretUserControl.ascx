<%@ Import Namespace="ContactManager.Models.Enums"%>
<%@ Import Namespace="ContactManager.Users.Services" %>
<%@ Import Namespace="ContactManager.Users.ViewModels" %>
<%@ Import Namespace="ContactManager.Models" %>
<%@ Import Namespace="ContactManager.Web.Helpers"%>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ClientViewModel>" %>
<%if (!Model.Role.Equals(ROLES.admin.ToString()) && Model.PPPSecret != null)
  {%>
<fieldset class="fields">
    <legend><a href="#" onclick="ShowDialog('../GetPopupData');">PPP Secret</a></legend>
    <%=Html.BuildItem("Local Address: ", Model.PPPSecret.LocalAddress)%>
    <p>
        <label for="LocalAddress">
            Local Address:</label>
        <%= Html.Encode(Model.PPPSecret.LocalAddress)%>
    </p>
    <p>
        <label for="RemoteAddress">
            Remote Address:</label>
        <%= Html.Encode(Model.PPPSecret.RemoteAddress)%>
    </p>
    <p>
        <label for="MACAddress">
            MAC Address:</label>
        <%= Html.Encode(Model.PPPSecret.MACAddress)%>
    </p>
    <p>
        <label for="DHCPAddress">
            DHCP Address:</label>
        <%= Html.Encode(Model.PPPSecret.DHCPAddress)%>
    </p>
</fieldset>
<%} %>
