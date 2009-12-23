<%@ Import Namespace="ContactManager.Users.ViewModels" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ClientViewModel>" %>
<%if (Model.Contract != null)
  {%>
<fieldset class="fields">
    <legend><a href="#">Contract</a></legend>
    <p>
        <label for="ContractNumber">
            ContractNumber:</label>
        <%= Html.Encode(Model.Contract.ContractNumber)%>
    </p>
    <p>
        <label for="Comment">
            Comment:</label>
        <%= Html.Encode(Model.Contract.Comment)%>
    </p>
    <p>
        <label for="CreateDate">
            CreateDate:</label>
        <%= Html.Encode(Model.Contract.CreateDate)%>
    </p>
    <p>
        <label for="ExpiredDate">
            ExpiredDate:</label>
        <%= Html.Encode(Model.Contract.ExpiredDate)%>
    </p>
</fieldset>
<%} %>