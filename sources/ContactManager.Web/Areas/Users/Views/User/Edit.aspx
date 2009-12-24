<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ContactManager.Users.ViewModels.ClientViewModel>" %>
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
    <%= Html.ValidationSummary("Edit was unsuccessful. Please correct the errors and try again.") %>
    <fieldset class="fields">
        <legend>Edit User:
            <%=Html.Encode(Model.UserName)%></legend>
        <fieldset class="fields">
            <legend>General</legend>
            <% using (Html.BeginForm())
               {%>
            <%= Html.Hidden("UserId", Model.UserId) %>
            <%= Html.Hidden("Balance", Model.Balance)%>
            <p>
                <label for="Balance">
                    Balance:</label>
                <%= Html.Encode(String.Format("{0:F}", Model.Balance)) %>
            </p>
            <p>
                <label for="UserName">
                    UserName:</label>
                <%= Html.TextBox("UserName", Model.UserName)%>
                <%= Html.ValidationMessage("UserName", "*")%>
            </p>
            <p>
                <label for="Password">
                    Password:</label>
                <%= Html.TextBox("Password", Model.Password) %>
                <%= Html.ValidationMessage("Password", "*") %>
                <input type="button" value="Generate" onclick="javascript: generatePassword('8', '#Password');" />
            </p>
            <p>
                <label for="Role">
                    Role:</label>
                <%= Html.DropDownListFor(r => r.Role, Model.Roles)%>
                <%= Html.ValidationMessage("Role", "*") %>
            </p>
            <%if (!Model.Role.Equals(ROLES.admin.ToString()))
              {%>
            <p>
                <label for="ProfileId">
                    Profile:</label>
                <%= Html.DropDownListFor(p => p.ProfileId, Model.Profiles)%>
                <%= Html.ValidationMessage("ProfileId", "*")%>
            </p>
            <%} %>
            <p>
                <label for="Credit">
                    Credit:</label>
                <%= Html.TextBox("Credit", String.Format("{0:F}", Model.Credit))%>
                <%= Html.ValidationMessage("Credit", "*")%>
            </p>
            <p>
                <label for="Email">
                    Email:</label>
                <%= Html.TextBox("Email", Model.Email) %>
                <%= Html.ValidationMessage("Email", "*") %>
            </p>
            <p>
                <label for="Comment">
                    Comment:</label>
                <%= Html.TextBox("Comment", Model.Comment)%>
                <%= Html.ValidationMessage("Comment", "*")%>
            </p>
            <p>
                <label for="StatusId">
                    Status:</label>
                <%= Html.DropDownList("StatusId", Model.Statuses)%>
                <%= Html.ValidationMessage("StatusId", "*")%>
            </p>
            <p>
                <input name="button" type="submit" value="Save" />
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
        <%=Html.ActionLink("Back to List", "Index") %>
    </div>
</asp:Content>
