<%@ Import Namespace="ContactManager.Web.Helpers"%>
<%@ Import Namespace="ContactManager.Users.ViewModels" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ClientViewModel>" %>
<%if (Model.Address != null)
  {%>
<fieldset class="fields">
    <legend><a href="#"><%=Html.Resource("Users_Resources, Users_Shared_AddressUserControl_Address")%></a></legend>
    <%=Html.BuildItem(Html.Resource("Users_Resources, Users_Shared_AddressUserControl_Address")+": ", string.Format("м.{0} вул.{1} дім {2} кв.{3}", Model.Address.City, Model.Address.Street.Name,  Model.Address.Building, Model.Address.Room ))%>

</fieldset>
<%} %>