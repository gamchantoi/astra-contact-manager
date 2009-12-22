<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<ContactManager.Models.MessageType>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table class="data-table" cellpadding="0" cellspacing="0">
        <tr>
            <th>
            </th>
            <%--            <th>
                TypeId
            </th>--%>
            <th>
                Name
            </th>
        </tr>
        <% foreach (var item in Model)
           { %>
        <tr>
            <td>
                <%= Html.ActionLink("Edit", "Edit", new { id=item.TypeId }) %>
<%--                |
                <%= Html.ActionLink("Delete", "Delete", new { id=item.TypeId })%>--%>
            </td>
            <%--            <td>
                <%= Html.Encode(item.TypeId) %>
            </td>--%>
            <td>
                <%= Html.Encode(item.Name) %>
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
