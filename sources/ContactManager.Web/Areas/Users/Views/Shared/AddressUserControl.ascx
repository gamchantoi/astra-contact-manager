<%@ Import Namespace="ContactManager.Web.Helpers" %>
<%@ Import Namespace="ContactManager.Users.ViewModels" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ClientViewModel>" %>
<fieldset class="fields">
    <%if (Model.Address != null)
      {%>
    <legend>
        <%=Html.JSLink(Html.Resource("Users_Resources, Users_User_Edit_Edit"), "ShowDialog", Url.Content("~/Addresses/Address/Edit/") + Model.UserId)%>
    </legend>
    <%=Html.BuildItem(Html.Resource("Users_Resources, Users_Shared_AddressUserControl_Address")+": ", 
        string.Format("м.{0} вул.{1} дім {2} кв.{3}", Model.Address.City, Model.Address.Street.Name,  Model.Address.Building, Model.Address.Room ))%>
    <%}%>
    <%else
        {%>
    <legend>
        <%=Html.JSLink(Html.Resource("Users_Resources, Users_User_Edit_Add"), "ShowDialog", Url.Content("~/Addresses/Address/Create/") + Model.UserId)%>
    </legend>
    <%}%>
</fieldset>
