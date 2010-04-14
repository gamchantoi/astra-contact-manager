<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ContactManager.Users.ViewModels.ClientViewModel>" %>

<%@ Import Namespace="ContactManager.Web.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script>
        $(document).ready(function() {
            $("#createForm").validate();
        });
    </script>

    <script language="javascript" type="text/javascript" src="<%= Url.Content("~/Scripts/astra-password-generator.js")%>"></script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%= Html.ValidationSummary(Html.Resource("Users_Resources, Users_User_Create_ValidationSummary"), new { @class = "ui-state-error ui-corner-all" }) %>
    
    <% using (Html.BeginForm("Create", "User", FormMethod.Post, new { id = "createForm" }))
       {%>
    <%--<fieldset class="fields">--%>
    <fieldset>
        <legend>
            <%= Html.Resource("Users_Resources, Users_User_Create_CreateNewUser")%></legend>
        <p>
            <label for="UserName">
                <%= Html.Resource("Users_Resources, Users_User_Create_UserName")%>:</label>
            <%= Html.TextBox("UserName", "", new { @class = "required" })%>
            <%= Html.ValidationMessage("UserName", "*") %>
        </p>
        <p>
            <label for="Password">
                <%= Html.Resource("Users_Resources, Users_User_Create_Password")%>:</label>
            <%= Html.TextBox("Password", "", new { @class = "required" })%>
            <%= Html.ValidationMessage("Password", "*") %>
            <%= Html.JSLink(Html.Resource("Users_Resources, Users_User_Create_Generate"), "generatePassword", "#Password")%>
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
            <%= Html.TextBox("Credit", 0, new { @class = "required" })%>
            <%= Html.ValidationMessage("Credit", "*")%>
        </p>
        <p>
            <label for="Email">
                <%= Html.Resource("Users_Resources, Users_User_Create_Email")%>:</label>
            <%= Html.TextBox("Email")%>
            <%= Html.ValidationMessage("Email", "*") %>
        </p>
        <p>
            <label for="StatusId">
                <%= Html.Resource("Users_Resources, Users_User_Create_Status")%>:</label>
            <%= Html.DropDownList("StatusId", Model.Statuses)%>
            <%= Html.ValidationMessage("StatusId", "*")%>
        </p>
        <p>
            <input type="submit" value='<%= Html.Resource("Users_Resources, Users_User_Create_Create")%>' />
        </p>
    </fieldset>
    <% } %>
    <div>
        <%=Html.ActionLink(Html.Resource("Users_Resources, Users_User_Create_BackToList"), "Index") %>
    </div>
</asp:Content>
