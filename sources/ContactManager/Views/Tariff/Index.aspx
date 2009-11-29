<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<ContactManager.Models.Tariff>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<%= Html.ValidationSummary("Deleting was unsuccessful.") %>
    <table class="data-table" cellpadding="0" cellspacing="0">
        <thead>
        <tr>
            <th>Select</th>
            <th>Delete</th>
            <th>
                Name
            </th>
            <th>
                Cost
            </th>
            <th>
                Service
            </th>
            <th>
                MaxUpload/MaxDownload
            </th>            
            <th>
                Direction
            </th>
            <th>
                Priority
            </th>
            <th>
                Limit_At
            </th>
        </tr>
        </thead>
        <tbody>
    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <a href='<%= Url.Action("Edit", new {id=item.TariffId}) %>'><img src="Content/Edit.png" alt="Edit" /></a>
            </td>
            <td>
                <a href='<%= Url.Action("Delete", new {id=item.TariffId}) %>'><img src="Content/Delete.png" alt="Delete" /></a>
            </td>
            <td>
                <%= Html.Encode(item.Name) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:F}", item.Cost)) %>
            </td>
            <td>
                <%= Html.Encode(item.Service) %>
            </td>
            <td>
                <%= Html.Encode(item.MaxUploadLimit) %>/<%= Html.Encode(item.MaxDownloadLimit) %>
            </td>
            <td>
                <%= Html.Encode(item.Direction) %>
            </td>
            <td>
                <%= Html.Encode(item.Priority) %>
            </td>
            <td>
                <%= Html.Encode(item.Limit_At) %>
            </td>
        </tr>
    
    <% } %>
    </tbody>
    </table>

    <p>
        <%= Html.ActionLink("Create New", "Create") %>
    </p>
    <p>
        <%= Html.ActionLink("Delete unused tariffs", "DeleteUnused")%>
    </p>

</asp:Content>

