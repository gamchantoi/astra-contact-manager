<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<ContactManager.Models.PaymentMethod>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <table  class="data-table" cellpadding="0" cellspacing="0">
        <tr>
            <th></th>
            <th>
                MethodId
            </th>
            <th>
                Name
            </th>
            <th>
                Comment
            </th>
            <th>
                Visible
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%= Html.ActionLink("Edit", "Edit", new { id=item.MethodId }) %> |
            </td>
            <td>
                <%= Html.Encode(item.MethodId) %>
            </td>
            <td>
                <%= Html.Encode(item.Name) %>
            </td>
            <td>
                <%= Html.Encode(item.Comment) %>
            </td>
            <td>
                <%= Html.Encode(item.Visible) %>
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

