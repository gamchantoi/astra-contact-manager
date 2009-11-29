<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ContactManager.Models.Service>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%= Html.ValidationSummary("Create was unsuccessful. Please correct the errors and try again.") %>
    <% using (Html.BeginForm())
       {%>
    <fieldset>
        <legend>Create New Service</legend>
        <p>
            <label for="Name">
                Name:</label>
            <%= Html.TextBox("Name") %>
            <%= Html.ValidationMessage("Name", "*") %>
        </p>
        <p>
            <label for="Comment">
                Comment:</label>
            <%= Html.TextBox("Comment")%>
            <%= Html.ValidationMessage("Comment", "*")%>
        </p>
        <p>
            <label for="Cost">
                Cost:</label>
            <%= Html.TextBox("Cost") %>
            <%= Html.ValidationMessage("Cost", "*") %>
        </p>
        <p>
            <label for="IsRegular">
                IsRegular:</label>
            <%= Html.CheckBox("IsRegular") %>
            <%= Html.ValidationMessage("IsRegular", "*") %>
        </p>
        <p>
            <label for="Active">
                Active:</label>
            <%= Html.CheckBox("Active", true)%>
            <%= Html.ValidationMessage("Active", "*")%>
        </p>
        <p>
            <label for="Visible">
                Visible:</label>
            <%= Html.CheckBox("Visible", true)%>
            <%= Html.ValidationMessage("Visible", "*")%>
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
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
