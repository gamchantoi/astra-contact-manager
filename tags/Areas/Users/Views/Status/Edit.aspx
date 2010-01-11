<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ContactManager.Models.Status>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%= Html.ValidationSummary("Edit was unsuccessful. Please correct the errors and try again.") %>

    <% using (Html.BeginForm()) {%>
    <%= Html.Hidden("StatusId", Model.StatusId) %>
        <fieldset>
            <legend>Edit status: <%= Html.Encode(Model.Name) %></legend>
            <p>
                <label for="DisplayName">Name:</label>
                <%= Html.TextBox("DisplayName", Model.DisplayName)%>
                <%= Html.ValidationMessage("DisplayName", "*")%>
            </p>
            <p>
                <input type="submit" value="Save" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%=Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>

