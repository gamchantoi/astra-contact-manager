<%@ Import Namespace="ContactManager.Users.ViewModels" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ClientViewModel>" %>
<%if (Model.Services != null && Model.Services.Count > 0)
  {%>
<fieldset class="fields">
    <legend><a href="#">Services</a></legend>
    <%
        foreach (var service in Model.Services)
        {%>
    <p>
        <label for="Name">
            Name:</label>
        <%= Html.Encode(service.Name)%>
    </p>
    <%} %>
</fieldset>
<%} %>