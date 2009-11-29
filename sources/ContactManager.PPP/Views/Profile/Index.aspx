<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<ContactManager.Models.Profile>>" %>

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
                    LocalAddress
                </th>
                <th>
                    Pool
                </th>
                <th>
                    RateLimit
                </th>
                <th>
                    Cost
                </th>
            </tr>
        </thead>
        <tbody>
        <% foreach (var item in Model)
           { %>
            <tr>
                <td>
                    <a href='<%= Url.Action("Delete", new {id=item.ProfileId}) %>'>
                        Delete</a>
                </td>
                <td>
                    <a href='<%= Url.Action("Edit", new {id=item.ProfileId}) %>'>
                        <%= Html.Encode(item.Name) %></a>
                </td>
                <td>
                    <%= Html.Encode(item.LocalAddress) %>
                </td>
                <td>
                    <%= Html.Encode(item.PoolName) %>
                </td>
                <td>
                    <%= Html.Encode(item.RateLimit) %>
                </td>
                <td>
                    <%= Html.Encode(String.Format("{0:F}", item.Cost)) %>
                </td>
            </tr>        
        <% } %>
        </tbody>
    </table>
    <p>
        <%= Html.ActionLink("Create New", "Create") %>    |
        <%= Html.ActionLink("Delete Profiles", "DeleteUnused")%>
    </p>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
