<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ContactManager.Users.ViewModels.ClientViewModel>" %>
<%@ Import Namespace="ContactManager.Web.Helpers"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript" src="<%= Url.Content("~/Scripts/astra-password-generator.js")%>"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%= Html.ValidationSummary(Html.Resource("Users_Resources, Users_User_Create_ValidationSummary")) %>
    <% using (Html.BeginForm())
       {%>
    <fieldset class="fields">
        <legend><%= Html.Resource("Users_Resources, Users_User_Create_CreateNewUser")%></legend>
        <p>
            <label for="UserName">
                <%= Html.Resource("Users_Resources, Users_User_Create_UserName")%>:</label>
            <%= Html.TextBox("UserName") %>
            <%= Html.ValidationMessage("UserName", "*") %>
        </p>
        <p>
            <label for="Password">
                <%= Html.Resource("Users_Resources, Users_User_Create_Password")%>:</label>
            <%= Html.TextBox("Password") %>
            <%= Html.ValidationMessage("Password", "*") %>
            <input type="button" value=<%= Html.Resource("Users_Resources, Users_User_Create_Generate")%> onclick="javascript: generatePassword('8', '#Password');" />
        </p>
        <p>
            <label for="Role">
                <%= Html.Resource("Users_Resources, Users_User_Create_Role")%>:</label>
            <%= Html.DropDownList("Role", Model.Roles )%>
            <%= Html.ValidationMessage("Role", "*") %>
        </p>
        <p>
            <label for="ProfileId">
                <%= Html.Resource("Users_Resources, Users_User_Create_Profile")%>:</label>
            <%= Html.DropDownList("ProfileId", Model.Profiles)%>
            <%= Html.ValidationMessage("ProfileId", "*")%>
        </p>
        <p>
            <label for="Balance">
                <%= Html.Resource("Users_Resources, Users_User_Create_Credit")%>:</label>
            <%= Html.TextBox("Credit", 0)%>
            <%= Html.ValidationMessage("Credit", "*")%>
        </p>
        <p>
            <label for="Email">
                <%= Html.Resource("Users_Resources, Users_User_Create_Email")%>:</label>
            <%= Html.TextBox("Email") %>
            <%= Html.ValidationMessage("Email", "*") %>
        </p>
        <p>
            <label for="StatusId">
                <%= Html.Resource("Users_Resources, Users_User_Create_Status")%>:</label>
            <%= Html.DropDownList("StatusId", Model.Statuses)%>
            <%= Html.ValidationMessage("StatusId", "*")%>
        </p>
        <p>
            <input type="submit" value=<%= Html.Resource("Users_Resources, Users_User_Create_Create")%> />
        </p>
    </fieldset>
    <% } %>
    <div>
        <%=Html.ActionLink(Html.Resource("Users_Resources, Users_User_Create_BackToList"), "Index") %>
    </div>
</asp:Content>
