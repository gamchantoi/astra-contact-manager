<%@ Import Namespace="ContactManager.Web.Helpers" %>
<%@ Import Namespace="ContactManager.Users.ViewModels" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ClientViewModel>" %>
<%if (Model.ClientDetails != null)
  {%>
<fieldset class="fields">
    <legend><a href="#"><%=Html.Resource("Users_Resources, Users_Shared_DetailsUserControl_Details")%></a></legend>
    <%=Html.BuildItem(Html.Resource("Users_Resources, Users_Shared_DetailsUserControl_FullName"), Model.FullName)%>
    <%=Html.BuildItem(Html.Resource("Users_Resources, Users_Shared_DetailsUserControl_Balance"), Model.Balance.ToString())%>
    <%=Html.BuildItem(Html.Resource("Users_Resources, Users_Shared_DetailsUserControl_Comment"), Model.Comment)%>
    <%=Html.BuildItem(Html.Resource("Users_Resources, Users_Shared_DetailsUserControl_Email"), Model.Email)%>
    <%=Html.BuildItem(Html.Resource("Users_Resources, Users_Shared_DetailsUserControl_Status"), Model.StatusDisplayName)%>
    <%=Html.BuildItem(Html.Resource("Users_Resources, Users_Shared_DetailsUserControl_Passport"), Model.ClientDetails.PassportID)%>
    <%=Html.BuildItem(Html.Resource("Users_Resources, Users_Shared_DetailsUserControl_PassportComment"), Model.ClientDetails.PassportComment)%>
    <%=Html.BuildItem(Html.Resource("Users_Resources, Users_Shared_DetailsUserControl_PassportDeliveryDate"), Model.ClientDetails.PassportDeliveryDate.HasValue ? Model.ClientDetails.PassportDeliveryDate.Value.ToShortDateString() : "")%>
</fieldset>
<%} %>