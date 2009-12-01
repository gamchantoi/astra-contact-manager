<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<ContactManager.Models.Host>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table class="data-table" cellpadding="0" cellspacing="0">
        <thead>
            <tr>
                <th>
                    Select
                </th>
                <th>
                    Delete
                </th>
                <th>
                    Address
                </th>
                <th>
                    UserName
                </th>
                <th>
                    UserPassword
                </th>
                <th>
                    LastUpdatedDate
                </th>
            </tr>
        </thead>
        <tbody>
            <% foreach (var item in Model)
               { %>
            <tr>
                <td>
                    <% if (HttpContext.Current.Profile.GetPropertyValue("HostId").Equals(item.HostId))
                       {%>
                    Selected
                    <% }
                       else
                       {%>
                    <%= Html.ActionLink("Select", "Select", new { id = item.HostId })%>
                    <%} %>
                </td>
                <td>
                    <a href='<%= Url.Action("Delete", new {id=item.HostId}) %>'>
                        Delete</a>
                </td>
                <td>
                    <a href='<%= Url.Action("Edit", new {id=item.HostId}) %>'>
                        <%= Html.Encode(item.Address) %></a>
                </td>
                <td>
                    <%= Html.Encode(item.UserName) %>
                </td>
                <td>
                    <%= Html.Encode(item.UserPassword) %>
                </td>
                <td>
                    <%= Html.Encode(String.Format("{0:g}", item.LastUpdatedDate)) %>
                </td>
            </tr>
            <% } %>
        </tbody>
    </table>
    <p>
        <%= Html.ActionLink("Create New", "Create") %>
    </p>
</asp:Content>
