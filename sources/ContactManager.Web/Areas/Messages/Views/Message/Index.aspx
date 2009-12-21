<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<ContactManager.Models.astra_Message>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Index</h2>

    <table>
        <tr>
            <th></th>
            <th>
                MessageId
            </th>
            <th>
                Text
            </th>
            <th>
                Title
            </th>
            <th>
                Date
            </th>
            <th>
                StatusId
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%= Html.ActionLink("Edit", "Edit", new { id=item.MessageId }) %> |
                <%= Html.ActionLink("Details", "Details", new { id=item.MessageId })%>
            </td>
            <td>
                <%= Html.Encode(item.MessageId) %>
            </td>
            <td>
                <%= Html.Encode(item.Text) %>
            </td>
            <td>
                <%= Html.Encode(item.Title) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:g}", item.Date)) %>
            </td>
            <td>
                <%= Html.Encode(item.StatusId) %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%= Html.ActionLink("Create New", "Create") %>
    </p>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>

