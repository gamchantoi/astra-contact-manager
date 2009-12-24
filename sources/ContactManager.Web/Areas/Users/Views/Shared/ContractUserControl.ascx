<%@ Import Namespace="ContactManager.Web.Helpers"%>
<%@ Import Namespace="ContactManager.Users.ViewModels" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ClientViewModel>" %>
<%if (Model.Contract != null)
  {%>
<fieldset class="fields">
    <legend><a href="#">Contract</a></legend>
    <%=Html.BuildItem("ContractNumber: ", Model.Contract.ContractNumber)%>
    <%=Html.BuildItem("Comment: ", Model.Contract.Comment)%>
    <%=Html.BuildItem("CreateDate: ", Model.Contract.CreateDate.HasValue ? Model.Contract.CreateDate.Value.ToShortDateString() :"" )%>
    <%=Html.BuildItem("ExpiredDate: ", Model.Contract.ExpiredDate.HasValue ? Model.Contract.ExpiredDate.Value.ToShortDateString() : "")%>
</fieldset>
<%} %> 