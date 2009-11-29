<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ContactManager.Models.Tariff>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%= Html.ValidationSummary("Create was unsuccessful. Please correct the errors and try again.") %>

    <% using (Html.BeginForm()) {%>

        <fieldset class="fields">
            <legend>Create new Tariff</legend>
            <p>
                <label for="Name">Name:</label>
                <%= Html.TextBox("Name") %>
                <%= Html.ValidationMessage("Name", "*") %>
            </p>
            <p>
                <label for="Description">Description:</label>
                <%= Html.TextArea("Description") %>
                <%= Html.ValidationMessage("Description", "*") %>
            </p>
            <p>
                <label for="Cost">Cost:</label>
                <%= Html.TextBox("Cost") %>
                <%= Html.ValidationMessage("Cost", "*") %>
            </p>
            <p>
                <label for="Service">Service:</label>
                <%= Html.TextBox("Service") %>
                <%= Html.ValidationMessage("Service", "*") %>
            </p>
            <p>
                <label for="MaxUploadLimit">MaxUploadLimit:</label>
                <%= Html.TextBox("MaxUploadLimit") %>
                <%= Html.ValidationMessage("MaxUploadLimit", "*") %>
            </p>
            <p>
                <label for="MaxDownloadLimit">MaxDownloadLimit:</label>
                <%= Html.TextBox("MaxDownloadLimit") %>
                <%= Html.ValidationMessage("MaxDownloadLimit", "*") %>
            </p>
            <p>
                <label for="Direction">Direction:</label>
                <%= Html.TextBox("Direction") %>
                <%= Html.ValidationMessage("Direction", "*") %>
            </p>
            <p>
                <label for="Priority">Priority:</label>
                <%= Html.TextBox("Priority") %>
                <%= Html.ValidationMessage("Priority", "*") %>
            </p>
            <p>
                <label for="Limit_At">Limit_At:</label>
                <%= Html.TextBox("Limit_At") %>
                <%= Html.ValidationMessage("Limit_At", "*") %>
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

