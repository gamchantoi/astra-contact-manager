<%@ Import Namespace="ContactManager.Web.Helpers" %>
<%@ Import Namespace="ContactManager.Users.ViewModels" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ClientViewModel>" %>
<fieldset class="fields">
    <%if (Model.Address != null)
      {%>
    <legend>
        <%=Html.SecureLink(Html.Resource("Users_Resources, Users_Shared_AddressUserControl_Address")
                        , Html.JSLink(Html.Resource("Users_Resources, Users_User_Edit_Edit"), "ShowDialog", Url.Content("~/Addresses/Address/Edit/") + Model.UserId))%>
    </legend>
    
    <%=Html.BuildItem(Html.Resource("Addresses_Resources, Addresses_View_Edit_City") + " ", Model.Address.City)%>
    <%=Html.BuildItem(Html.Resource("Addresses_Resources, Addresses_View_Edit_Street") + " ", Model.Address.Street.Name)%>
    <%=Html.BuildItem(Html.Resource("Addresses_Resources, Addresses_View_Edit_Building") + " ", Model.Address.Building)%>
    <%=Html.BuildItem(Html.Resource("Addresses_Resources, Addresses_View_Edit_Room") + " ", Model.Address.Room)%>
        <%}%>
    <%else
        {%>
    <legend>
        <%=Html.SecureLink(Html.Resource("Users_Resources, Users_Shared_AddressUserControl_Address")
                        , Html.JSLink(Html.Resource("Users_Resources, Users_User_Edit_Add"), "ShowDialog", Url.Content("~/Addresses/Address/Create/") + Model.UserId))%>
    </legend>
    <%}%>
</fieldset>
