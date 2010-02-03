<%@ Import Namespace="ContactManager.Web.Helpers" %>
<%@ Import Namespace="ContactManager.Users.ViewModels" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ClientViewModel>" %>
<fieldset class="fields">
    <%if (Model.Contract != null)
      {%>
                <legend>
        <%=Html.JSLink(Html.Resource("Users_Resources, Users_User_Edit_Edit"), "ShowDialog", Url.Content("~/Users/Contract/Edit/") + Model.UserId)%>
    </legend>
    <%--<legend><a href="#" onclick="ShowDialog('../../../Users/Contract/Edit/<%= Model.UserId %>');">
        <%= Html.Resource("Users_Resources, Users_User_Edit_Edit")%></a></legend>--%>
    <%=Html.BuildItem(Html.Resource("Users_Resources, Users_Shared_ContractUserControl_ContractNumber")+": ", Model.Contract.ContractNumber)%>
    <%=Html.BuildItem(Html.Resource("Users_Resources, Users_Shared_ContractUserControl_Comment")+": ", Model.Contract.Comment)%>
    <%=Html.BuildItem(Html.Resource("Users_Resources, Users_Shared_ContractUserControl_CreateDate")+": ", Model.Contract.CreateDate.HasValue ? Model.Contract.CreateDate.Value.ToShortDateString() :"" )%>
    <%=Html.BuildItem(Html.Resource("Users_Resources, Users_Shared_ContractUserControl_ExpiredDate")+": ", Model.Contract.ExpiredDate.HasValue ? Model.Contract.ExpiredDate.Value.ToShortDateString() : "")%>
    <%}%>
    <%else
        {%>
                        <legend>
        <%=Html.JSLink(Html.Resource("Users_Resources, Users_User_Edit_Add"), "ShowDialog", Url.Content("~/Users/Contract/Create/") + Model.UserId)%>
    </legend>
<%--        <legend><a href="#" onclick="ShowDialog('../../../Users/Contract/Create/<%= Model.UserId %>');">
        <%= Html.Resource("Users_Resources, Users_User_Edit_Add")%></a></legend>--%>
    <%} %>
</fieldset>
