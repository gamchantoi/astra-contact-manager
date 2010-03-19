<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<ContactManager.Models.Status>" %>


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

