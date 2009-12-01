<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ContactManager.Models.Address>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%= Html.ValidationSummary("Create was unsuccessful. Please correct the errors and try again.") %>
    <% using (Html.BeginForm("Create", "Address"))
       {%>
    <fieldset class="fields">
        <legend>Create address</legend>
        <p>
            <label for="City">
                City:</label>
            <%= Html.TextBox("City") %>
            <%= Html.ValidationMessage("City", "*") %>
        </p>
        <p>
            <label for="Street.StreetId">
                Street:</label>
            <%= Html.DropDownList("Street.StreetId", (SelectList)ViewData["Streets"])%>
            <%= Html.ValidationMessage("Street.StreetId", "*")%>
            <%= Html.ActionLink("Add New", "CreateStreet")%>
        </p>
        <p>
            <label for="Building">
                Building:</label>
            <%= Html.TextBox("Building") %>
            <%= Html.ValidationMessage("Building", "*") %>
        </p>
        <p>
            <label for="Details">
                Details:</label>
            <%= Html.TextBox("Details") %>
            <%= Html.ValidationMessage("Details", "*") %>
        </p>
        <p>
            <label for="Room">
                Room:</label>
            <%= Html.TextBox("Room") %>
            <%= Html.ValidationMessage("Room", "*") %>
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
