<%@ Import Namespace="ContactManager.Web.Helpers" %>
<%@ Import Namespace="ContactManager.Users.ViewModels" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ClientViewModel>" %>
<fieldset class="fields">

    <%if (Model.Services != null && Model.Services.Count > 0)
      {%>
    <legend><a href="#" onclick="ShowDialog('../../../Services/Service/ClientServices/<%= Model.UserId %>');">
        <%= Html.Resource("Users_Resources, Users_User_Edit_Edit")%></a></legend>
    <%foreach (var service in Model.Services)
        {%>
    <%=Html.BuildItem(Html.Resource("Users_Resources, Users_Shared_ServicesUserControl_Name")+": ", 
        string.Format("{0} ({1:F})", service.Name, service.Cost))%>
    <%}%>
    <%}%>
    <% else
        {%>
    <legend><a href="#" onclick="ShowDialog('../../../Services/Service/ClientServices/<%= Model.UserId %>');">
        <%= Html.Resource("Users_Resources, Users_User_Edit_Add") %></a></legend>
    <%} %>
</fieldset>
