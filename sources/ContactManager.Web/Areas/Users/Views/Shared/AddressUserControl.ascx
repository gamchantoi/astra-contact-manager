<%@ Import Namespace="ContactManager.Users.ViewModels" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ClientViewModel>" %>
<%if (Model.Address != null)
  {%>
<fieldset class="fields">
    <legend><a href="#">Address</a></legend>
    <p>
        <label for="City">
            City:</label>
        <%= Html.Encode(Model.Address.City)%>
    </p>
    <p>
        <label for="StreetId">
            Street:</label>
        <%= Html.Encode(Model.Address.Street.Name)%>
    </p>
    <p>
        <label for="Building">
            Building:</label>
        <%= Html.Encode(Model.Address.Building)%>
    </p>
    <p>
        <label for="Details">
            Details:</label>
        <%= Html.Encode(Model.Address.Details)%>
    </p>
</fieldset>
<%} %>