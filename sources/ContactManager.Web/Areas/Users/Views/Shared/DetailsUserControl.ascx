<%@ Import Namespace="ContactManager.Web.Helpers" %>
<%@ Import Namespace="ContactManager.Users.ViewModels" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ClientViewModel>" %>
<%if (Model.ClientDetails != null)
  {%>
<fieldset class="fields">
    <legend><a href="#">Details</a></legend>
    <%=Html.BuildItem("Full Name: ", Model.FullName)%>
    <%=Html.BuildItem("Balance: ", Model.Balance.ToString())%>
    <%=Html.BuildItem("Comment: ", Model.Comment)%>
    <%=Html.BuildItem("Email: ", Model.Email)%>
    <%=Html.BuildItem("Status: ", Model.StatusDisplayName)%>
    <%=Html.BuildItem("Passport: ", Model.ClientDetails.PassportID)%>
    <%=Html.BuildItem("Passport Comment: ", Model.ClientDetails.PassportComment)%>
    <%=Html.BuildItem("Passport Delivery Date: ", Model.ClientDetails.PassportDeliveryDate.HasValue ? Model.ClientDetails.PassportDeliveryDate.Value.ToShortDateString() : "")%>
</fieldset>
<%} %>