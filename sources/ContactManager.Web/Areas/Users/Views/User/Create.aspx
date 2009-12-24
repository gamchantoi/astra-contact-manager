<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ContactManager.Users.ViewModels.ClientViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript" src="<%= Url.Content("~/Scripts/PasswordGenerator.js")%>"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%= Html.ValidationSummary("Create was unsuccessful. Please correct the errors and try again.") %>
    <% using (Html.BeginForm())
       {%>
    <fieldset class="fields">
        <legend>Create New User</legend>
        <p>
            <label for="UserName">
                UserName:</label>
            <%= Html.TextBox("UserName") %>
            <%= Html.ValidationMessage("UserName", "*") %>
        </p>
        <p>
            <label for="Password">
                Password:</label>
            <%= Html.TextBox("Password") %>
            <%= Html.ValidationMessage("Password", "*") %>
            <input type="button" value="Generate" onclick="javascript: generatePassword('8', '#Password');" />
        </p>
        <p>
            <label for="Role">
                Role:</label>
            <%= Html.DropDownList("Role", Model.Roles )%>
            <%= Html.ValidationMessage("Role", "*") %>
        </p>
        <p>
            <label for="ProfileId">
                Profile:</label>
            <%= Html.DropDownList("ProfileId", Model.Profiles)%>
            <%= Html.ValidationMessage("ProfileId", "*")%>
        </p>
        <p>
            <label for="Balance">
                Credit:</label>
            <%= Html.TextBox("Credit", 0)%>
            <%= Html.ValidationMessage("Credit", "*")%>
        </p>
        <p>
            <label for="Email">
                Email:</label>
            <%= Html.TextBox("Email") %>
            <%= Html.ValidationMessage("Email", "*") %>
        </p>
        <p>
            <label for="StatusId">
                Status:</label>
            <%= Html.DropDownList("StatusId", Model.Statuses)%>
            <%= Html.ValidationMessage("StatusId", "*")%>
        </p>
        <p>
            <input type="submit" value="Create" />
        </p>
    </fieldset>
    <% } %>
    <div>
        <%=Html.ActionLink("Back to List", "Index") %>
    </div>
</asp:Content>
