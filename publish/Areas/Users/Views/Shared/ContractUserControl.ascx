﻿<%@ Import Namespace="ContactManager.Web.Helpers"%>
<%@ Import Namespace="ContactManager.Users.ViewModels" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ClientViewModel>" %>
<%if (Model.Contract != null)
  {%>
<fieldset class="fields">
    <legend><a href="#"><%=Html.Resource("Users_Resources, Users_Shared_ContractUserControl_Contract")%></a></legend>
    <%=Html.BuildItem(Html.Resource("Users_Resources, Users_Shared_ContractUserControl_ContractNumber")+": ", Model.Contract.ContractNumber)%>
    <%=Html.BuildItem(Html.Resource("Users_Resources, Users_Shared_ContractUserControl_Comment")+": ", Model.Contract.Comment)%>
    <%=Html.BuildItem(Html.Resource("Users_Resources, Users_Shared_ContractUserControl_CreateDate")+": ", Model.Contract.CreateDate.HasValue ? Model.Contract.CreateDate.Value.ToShortDateString() :"" )%>
    <%=Html.BuildItem(Html.Resource("Users_Resources, Users_Shared_ContractUserControl_ExpiredDate")+": ", Model.Contract.ExpiredDate.HasValue ? Model.Contract.ExpiredDate.Value.ToShortDateString() : "")%>
</fieldset>
<%} %> 