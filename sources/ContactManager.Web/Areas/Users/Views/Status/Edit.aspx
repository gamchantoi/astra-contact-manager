<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<ContactManager.Models.Status>" %>

    <script>
        $(document).ready(function() {
            $("#createForm").validate();
        });
    </script>
    
    
    <%= Html.ValidationSummary("Edit was unsuccessful. Please correct the errors and try again.") %>

    <% using (Html.BeginForm("Edit", "Status", FormMethod.Post, new { id = "createForm" }))
       {%>
    <%= Html.Hidden("StatusId", Model.StatusId) %>
        <fieldset>
            <legend>Edit status: <%= Html.Encode(Model.Name) %></legend>
            <p>
                <label for="DisplayName">Name:</label>
                <%= Html.TextBox("DisplayName", Model.DisplayName, new { @class = "required" })%>
                <%= Html.ValidationMessage("DisplayName", "*")%>
            </p>
            <p>
                <input type="submit" value="Save" />
            </p>
        </fieldset>

    <% } %>

