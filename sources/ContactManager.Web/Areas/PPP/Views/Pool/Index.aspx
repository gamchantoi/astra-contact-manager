<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<ContactManager.Models.Pool>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%= Html.ValidationSummary("Operation was unsuccessful. Please view details and try again.")%>
    <table class="data-table" cellpadding="0" cellspacing="0">
        <thead>
            <tr>
                <th>
                    Delete
                </th>
                <th>
                    Name
                </th>
                <th>
                    Addresses
                </th>
                <th>
                    NextPool
                </th>
            </tr>
        </thead>
        <tbody>
        <% foreach (var item in Model)
           { %>
        
            <tr>
                <td>
                    <a href='<%= Url.Action("Delete", new {id=item.PoolId}) %>'>Delete</a>
                </td>
                <td>
                    <a href='<%= Url.Action("Edit", new {id=item.PoolId}) %>'>
                        <%= Html.Encode(item.Name) %></a>
                </td>
                <td>
                    <%= Html.Encode(item.Addresses) %>
                </td>
                <td>
                    <%= Html.Encode(item.NextPoolName) %>
                </td>
            </tr>
            <% } %>
        </tbody>
    </table>
    <p>
        <%= Html.ActionLink("Create New", "Create") %> | 
        <%= Html.ActionLink("Delete All", "DeleteAll")%>
    </p>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
