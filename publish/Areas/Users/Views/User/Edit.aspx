<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ContactManager.Users.ViewModels.ClientViewModel>" %>
<%@ Import Namespace="ContactManager.Web.Helpers"%>
<%@ Import Namespace="ContactManager.Models.Enums"%>

<%@ Import Namespace="ContactManager.Users.Services" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript" src="<%= Url.Content("~/Scripts/PasswordGenerator.js")%>"></script>
    <script language="javascript" type="text/javascript" src="<%= Url.Content("~/Scripts/jquery-ui-1.7.2/ui/ui.core.js")%>"></script>
    <script language="javascript" type="text/javascript" src="<%= Url.Content("~/Scripts/jquery-ui-1.7.2/ui/ui.dialog.js")%>"></script>
    <script language="javascript" type="text/javascript" src="<%= Url.Content("~/Scripts/jquery-ui-1.7.2/ui/ui.draggable.js")%>"></script>
    <script language="javascript" type="text/javascript" src="<%= Url.Content("~/Scripts/jquery-ui-1.7.2/ui/ui.resizable.js")%>"></script>
    <script language="javascript" type="text/javascript" src="<%= Url.Content("~/Scripts/astra-dialog.js")%>"></script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%= Html.ValidationSummary(Html.Resource("Users_Resources, Users_User_Edit_ValidationSummary"))%>
    <fieldset class="fields">
        <legend><%= Html.Resource("Users_Resources, Users_User_Edit_EditUser")%>
            <%=Html.Encode(Model.UserName)%></legend>
        <fieldset class="fields">
            <legend><%= Html.Resource("Users_Resources, Users_User_Edit_General")%></legend>
            <% using (Html.BeginForm())
               {%>
            <%= Html.Hidden("UserId", Model.UserId) %>
            <%= Html.Hidden("Balance", Model.Balance)%>
            <p>
                <label for="Balance">
                    <%= Html.Resource("Users_Resources, Users_User_Edit_Balance")%>:</label>
                <%= Html.Encode(String.Format("{0:F}", Model.Balance)) %>
            </p>
            <p>
                <label for="UserName">
                    <%= Html.Resource("Users_Resources, Users_User_Edit_UserName")%>:</label>
                <%= Html.TextBox("UserName", Model.UserName)%>
                <%= Html.ValidationMessage("UserName", "*")%>
            </p>
            <p>
                <label for="Password">
                    <%= Html.Resource("Users_Resources, Users_User_Edit_Password")%>:</label>
                <%= Html.TextBox("Password", Model.Password) %>
                <%= Html.ValidationMessage("Password", "*") %>
                <input type="button" value="Generate" onclick="javascript: generatePassword('8', '#Password');" />
            </p>
            <p>
                <label for="Role">
                    <%= Html.Resource("Users_Resources, Users_User_Edit_Role")%>:</label>
                <%= Html.DropDownListFor(r => r.Role, Model.Roles)%>
                <%= Html.ValidationMessage("Role", "*") %>
            </p>
            <%if (!Model.Role.Equals(ROLES.admin.ToString()))
              {%>
            <p>
                <label for="ProfileId">
                    <%= Html.Resource("Users_Resources, Users_User_Edit_Profile")%>:</label>
                <%= Html.DropDownListFor(p => p.ProfileId, Model.Profiles)%>
                <%= Html.ValidationMessage("ProfileId", "*")%>
            </p>
            <%} %>
            <p>
                <label for="Credit">
                    <%= Html.Resource("Users_Resources, Users_User_Edit_Credit")%>:</label>
                <%= Html.TextBox("Credit", String.Format("{0:F}", Model.Credit))%>
                <%= Html.ValidationMessage("Credit", "*")%>
            </p>
            <p>
                <label for="Email">
                    <%= Html.Resource("Users_Resources, Users_User_Edit_Email")%>:</label>
                <%= Html.TextBox("Email", Model.Email) %>
                <%= Html.ValidationMessage("Email", "*") %>
            </p>
            <p>
                <label for="Comment">
                    <%= Html.Resource("Users_Resources, Users_User_Edit_Comment")%>:</label>
                <%= Html.TextBox("Comment", Model.Comment)%>
                <%= Html.ValidationMessage("Comment", "*")%>
            </p>
            <p>
                <label for="StatusId">
                    <%= Html.Resource("Users_Resources, Users_User_Edit_Status")%>:</label>
                <%= Html.DropDownListFor(s => s.StatusId, Model.Statuses)%>
                <%= Html.ValidationMessage("StatusId", "*")%>
            </p>
            <p>
                <input name="button" type="submit" value="<%= Html.Resource("Users_Resources, Users_User_Edit_Save")%>" />
            </p>
            <% } %>
        </fieldset>
        <%Html.RenderPartial("PPPSecretUserControl", Model); %>
        <%Html.RenderPartial("DetailsUserControl", Model); %>
        <%Html.RenderPartial("ServicesUserControl", Model); %>
        <%Html.RenderPartial("AddressUserControl", Model); %>
        <%Html.RenderPartial("ContractUserControl", Model); %>
    </fieldset>
    <div>
        <%=Html.ActionLink(Html.Resource("Users_Resources, Users_User_Edit_BackToList"), "Index") %>
    </div>
</asp:Content>
