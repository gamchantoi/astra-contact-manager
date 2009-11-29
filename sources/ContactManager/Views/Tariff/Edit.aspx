<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ContactManager.Models.Tariff>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%= Html.ValidationSummary("Edit was unsuccessful. Please correct the errors and try again.") %>

    <% using (Html.BeginForm()) {%>
    <%= Html.Hidden("TariffId", Model.TariffId) %>
        <fieldset class="fields">
            <legend>Edit Tariff: <%= Html.Encode(Model.Name) %></legend>
            <p>
                <label for="Name">Name:</label>
                <%= Html.TextBox("Name", Model.Name) %>
                <%= Html.ValidationMessage("Name", "*") %>
            </p>
            <p>
                <label for="Description">Description:</label>
                <%= Html.TextArea("Description", Model.Description) %>
                <%= Html.ValidationMessage("Description", "*") %>
            </p>
            <p>
                <label for="Cost">Cost:</label>
                <%= Html.TextBox("Cost", String.Format("{0:F}", Model.Cost)) %>
                <%= Html.ValidationMessage("Cost", "*") %>
            </p>
            <p>
                <label for="Service">Service:</label>
                <%= Html.TextBox("Service", Model.Service) %>
                <%= Html.ValidationMessage("Service", "*") %>
            </p>
            <p>
                <label for="Profile">Profile:</label>
                <%= Html.TextBox("Profile", Model.Profile) %>
                <%= Html.ValidationMessage("Profile", "*") %>
            </p>
            <p>
                <label for="MaxUploadLimit">MaxUploadLimit:</label>
                <%= Html.TextBox("MaxUploadLimit", Model.MaxUploadLimit) %>
                <%= Html.ValidationMessage("MaxUploadLimit", "*") %>
            </p>
            <p>
                <label for="MaxDownloadLimit">MaxDownloadLimit:</label>
                <%= Html.TextBox("MaxDownloadLimit", Model.MaxDownloadLimit) %>
                <%= Html.ValidationMessage("MaxDownloadLimit", "*") %>
            </p>
            <p>
                <label for="Direction">Direction:</label>
                <%= Html.TextBox("Direction", Model.Direction) %>
                <%= Html.ValidationMessage("Direction", "*") %>
            </p>
            <p>
                <label for="Priority">Priority:</label>
                <%= Html.TextBox("Priority", Model.Priority) %>
                <%= Html.ValidationMessage("Priority", "*") %>
            </p>
            <p>
                <label for="QueueType">QueueType:</label>
                <%= Html.TextBox("QueueType", Model.QueueType) %>
                <%= Html.ValidationMessage("QueueType", "*") %>
            </p>
            <p>
                <label for="Limit_At">Limit_At:</label>
                <%= Html.TextBox("Limit_At", Model.Limit_At) %>
                <%= Html.ValidationMessage("Limit_At", "*") %>
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

