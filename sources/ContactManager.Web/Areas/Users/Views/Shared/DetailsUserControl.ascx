<%@ Import Namespace="ContactManager.Models.Enums" %>
<%@ Import Namespace="ContactManager.Web.Helpers" %>
<%@ Import Namespace="ContactManager.Users.ViewModels" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ClientViewModel>" %>
<fieldset class="fields">

    <%if (Model.ClientDetails != null)
      {%>
        <legend><a href="#" onclick="ShowDialog('../../../Users/Detail/Edit/<%= Model.UserId %>');">
            <%=Html.Resource("Users_Resources, Users_User_Edit_Edit")%></a> </legend>
        <%if (!Model.Role.Equals(ROLES.admin.ToString()) && Model.PPPSecret != null)
          {%>
        <%=Html.BuildItem(Html.Resource("Users_Resources, Users_Shared_DetailsUserControl_FullName"), Model.FullName)%>
        <%=Html.BuildItem(Html.Resource("Users_Resources, Users_Shared_DetailsUserControl_Email"), Model.Email)%>
        <%=Html.BuildItem(Html.Resource("Users_Resources, Users_Shared_DetailsUserControl_Passport"), Model.ClientDetails.PassportID)%>
        <%=Html.BuildItem(Html.Resource("Users_Resources, Users_Shared_DetailsUserControl_PassportComment"), Model.ClientDetails.PassportComment)%>
        <%=Html.BuildItem(Html.Resource("Users_Resources, Users_Shared_DetailsUserControl_PassportDeliveryDate"), Model.ClientDetails.PassportDeliveryDate.HasValue ? Model.ClientDetails.PassportDeliveryDate.Value.ToShortDateString() : "")%>
        <%} %>
    <%}
      else
      {%>
      <legend><a href="#" onclick="ShowDialog('../../../Users/Detail/Create/<%= Model.UserId %>');">
        <%=Html.Resource("Users_Resources, Users_User_Edit_Add")%></a> </legend>
      <%} %>
</fieldset>
