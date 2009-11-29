<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<ContactManager.Models.Service>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table class="data-table" cellpadding="0" cellspacing="0">
        <thead>
            <tr>
                <th>
                    Type
                </th>
                <th>
                    Name
                </th>
                <th>
                    Comment
                </th>
                <th>
                    Cost
                </th>
                <th>
                    Status
                </th>
            </tr>
        </thead>
        <tbody>
            <% foreach (var item in Model)
               { %>
            <tr>
                <td>
                    <%= Html.Encode(item.IsRegular ? "Regular" : "Once") %>
                </td>
                <td>
                    <a href='<%= Url.Action("Edit", new {id=item.ServiceId}) %>'><%= Html.Encode(item.Name)%></a>
                    <%if(!string.IsNullOrEmpty(item.SystemData)){ %>
                    (System)
                    <%} %>
                </td>
                 <td>
                    <%= Html.Encode(item.Comment) %>
                </td>
                <td>
                    <%= Html.Encode(String.Format("{0:F}", item.Cost)) %>
                </td>
                <td>
                    <%= Html.Encode(item.Active ? "Active" : "Disable")%>
                </td>
            </tr>
            <% } %>
        </tbody>
    </table>
    <p>
        <%= Html.ActionLink("Create New", "Create") %>&nbsp;|&nbsp;
        <%= Html.ActionLink("Service Activity", "Activities")%>
    </p>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
