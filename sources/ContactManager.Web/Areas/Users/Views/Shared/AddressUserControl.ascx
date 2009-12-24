<%@ Import Namespace="ContactManager.Web.Helpers"%>
<%@ Import Namespace="ContactManager.Users.ViewModels" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ClientViewModel>" %>
<%if (Model.Address != null)
  {%>
<fieldset class="fields">
    <legend><a href="#">Address</a></legend>
    <%=Html.BuildItem("Address: ", string.Format("м.{0} вул.{1} дім {2} кв.{3}", Model.Address.City, Model.Address.Street.Name,  Model.Address.Building, Model.Address.Room ))%>

</fieldset>
<%} %>