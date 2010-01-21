<%@ Import Namespace="ContactManager.Web.Helpers"%>
<%@ Import Namespace="ContactManager.Users.ViewModels" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ClientViewModel>" %>


<fieldset class="fields">
    <legend><a href="#" onclick="ShowDialog('../../../Services/Service/ClientServices/<%= Model.UserId %>');">
    <%if (Model.Services != null && Model.Services.Count > 0)
  {%>
    <%=Html.Resource("Users_Resources, Users_Shared_ServicesUserControl_Services")%></a></legend>
    <%
        foreach (var service in Model.Services)
        {%>
        <%=Html.BuildItem(Html.Resource("Users_Resources, Users_Shared_ServicesUserControl_Name")+": ", string.Format("{0} ({1:F})", service.Name, service.Cost))%>
    <%} %>
    <%} %>
</fieldset>
