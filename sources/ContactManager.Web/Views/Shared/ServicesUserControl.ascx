<%@ Import Namespace="ContactManager.Web.Helpers" %>
<%@ Import Namespace="ContactManager.Users.ViewModels" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ClientViewModel>" %>
<fieldset class="fields">

    <%if (Model.Services != null && Model.Services.Count > 0)
      {%>
          <legend>
        <%=Html.JSLink(Html.Resource("Users_Resources, Users_User_Edit_Edit"), "ShowDialog", Url.Content("~/Services/Service/ClientServices/") + Model.UserId)%>
    </legend>
    
<%--    <legend><a href="#" onclick="ShowDialog('../../../Services/Service/ClientServices/<%= Model.UserId %>');">
        <%= Html.Resource("Users_Resources, Users_User_Edit_Edit")%></a></legend>--%>
    <%foreach (var service in Model.Services)
        {%>
    <%=Html.BuildItem(Html.Resource("Users_Resources, Users_Shared_ServicesUserControl_Name")+": ", 
        string.Format("{0} ({1:F})", service.Name, service.Cost))%>
    <%}%>
    <%}%>
    <% else
        {%>
                  <legend>
        <%=Html.JSLink(Html.Resource("Users_Resources, Users_User_Edit_Add"), "ShowDialog", Url.Content("~/Services/Service/ClientServices/") + Model.UserId)%>
    </legend>
<%--    <legend><a href="#" onclick="ShowDialog('../../../Services/Service/ClientServices/<%= Model.UserId %>');">
        <%= Html.Resource("Users_Resources, Users_User_Edit_Add") %></a></legend>--%>
    <%} %>
</fieldset>
