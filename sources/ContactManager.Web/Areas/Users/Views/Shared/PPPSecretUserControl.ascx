<%@ Import Namespace="Resources" %>
<%@ Import Namespace="ContactManager.Models.Enums" %>
<%@ Import Namespace="ContactManager.Users.Services" %>
<%@ Import Namespace="ContactManager.Users.ViewModels" %>
<%@ Import Namespace="ContactManager.Models" %>
<%@ Import Namespace="ContactManager.Web.Helpers" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ClientViewModel>" %>
<fieldset class="fields">
    <legend><a href="#" onclick="ShowDialog('../../../PPP/Profile/EditSecret/<%= Model.PPPSecret == null ? Guid.Empty : Model.PPPSecret.UserId %>');">
        <%=Html.Resource("Users_Resources, Users_Shared_PPPSecretUserControl_PPPSecret")%></a></legend>
    <%if (!Model.Role.Equals(ROLES.admin.ToString()) && Model.PPPSecret != null)
      {%>
    <%=Html.BuildItem(Html.Resource("Users_Resources, Users_Shared_PPPSecretUserControl_Name"), Model.PPPSecret.Name)%>
    <%=Html.BuildItem(Html.Resource("Users_Resources, Users_Shared_PPPSecretUserControl_Password"), Model.PPPSecret.Password)%>
    <%=Html.BuildItem(Html.Resource("Users_Resources, Users_Shared_PPPSecretUserControl_LocalAddress"), Model.PPPSecret.LocalAddress)%>
    <%=Html.BuildItem(Html.Resource("Users_Resources, Users_Shared_PPPSecretUserControl_RemoteAddress"), Model.PPPSecret.RemoteAddress)%>
    <%=Html.BuildItem(Html.Resource("Users_Resources, Users_Shared_PPPSecretUserControl_MACAddress"), Model.PPPSecret.MACAddress)%>
    <%=Html.BuildItem(Html.Resource("Users_Resources, Users_Shared_PPPSecretUserControl_DHCPAddress"), Model.PPPSecret.DHCPAddress)%>
    <%=Html.BuildItem(Html.Resource("Users_Resources, Users_Shared_PPPSecretUserControl_Comment"), Model.PPPSecret.Comment)%>
    <%=Html.BuildItem(Html.Resource("Users_Resources, Users_Shared_PPPSecretUserControl_Routes"), Model.PPPSecret.Routes)%>
    <%} %>
</fieldset>
