<%@ Import Namespace="ContactManager.Users.ViewModels"%>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ClientViewModel>" %>
<%if (Model.ClientDetails != null)
  {%>
<fieldset class="fields">
    <legend><a href="#">Details</a></legend>
    <p>
        <label for="FullName">
            Full Name:</label>
        <%= Html.Encode(Model.FullName)%>
    </p>
    <p>
        <label for="Passport">
            Passport:</label>
        <%= Html.Encode(Model.ClientDetails.PassportID)%>
    </p>
    <p>
        <label for="PassportComment">
            Passport Comment:</label>
        <%= Html.Encode(Model.ClientDetails.PassportComment)%>
    </p>
    <p>
        <label for="PassportDeliveryDate">
            Passport Delivery Date:</label>
        <%= Html.Encode(Model.ClientDetails.PassportDeliveryDate)%>
    </p>
</fieldset>
<%} %>