<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<ContactManager.Models.Contract>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Index</h2>

    <table>
        <tr>
            <th></th>
            <th>
                ContractId
            </th>
            <th>
                ContractNumber
            </th>
            <th>
                Comment
            </th>
            <th>
                CreateDate
            </th>
            <th>
                ExpiredDate
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%= Html.ActionLink("Edit", "Edit", new { id=item.ContractId }) %> |
                <%= Html.ActionLink("Details", "Details", new { id=item.ContractId })%>
            </td>
            <td>
                <%= Html.Encode(item.ContractId) %>
            </td>
            <td>
                <%= Html.Encode(item.ContractNumber) %>
            </td>
            <td>
                <%= Html.Encode(item.Comment) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:g}", item.CreateDate)) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:g}", item.ExpiredDate)) %>
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

