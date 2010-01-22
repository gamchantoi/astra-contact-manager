<%@ Import Namespace="ContactManager.Web.Helpers" %>
<%@ Import Namespace="ContactManager.Users.ViewModels" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ClientViewModel>" %>
<fieldset class="fields">
    <%if (Model.Contract != null)
      {%>
    <legend><a href="#" onclick="ShowDialog('../../../Users/Contract/Edit/<%= Model.UserId %>');">
        <%= Html.Resource("Users_Resources, Users_User_Edit_Edit")%></a></legend>
    <%=Html.BuildItem(Html.Resource("Users_Resources, Users_Shared_ContractUserControl_ContractNumber")+": ", Model.Contract.ContractNumber)%>
    <%=Html.BuildItem(Html.Resource("Users_Resources, Users_Shared_ContractUserControl_Comment")+": ", Model.Contract.Comment)%>
    <%=Html.BuildItem(Html.Resource("Users_Resources, Users_Shared_ContractUserControl_CreateDate")+": ", Model.Contract.CreateDate.HasValue ? Model.Contract.CreateDate.Value.ToShortDateString() :"" )%>
    <%=Html.BuildItem(Html.Resource("Users_Resources, Users_Shared_ContractUserControl_ExpiredDate")+": ", Model.Contract.ExpiredDate.HasValue ? Model.Contract.ExpiredDate.Value.ToShortDateString() : "")%>
    <%}%>
    <%else
        {%>
        <legend><a href="#" onclick="ShowDialog('../../../Users/Contract/Create/<%= Model.UserId %>');">
        <%= Html.Resource("Users_Resources, Users_User_Edit_Add")%></a></legend>
    <%} %>
</fieldset>
