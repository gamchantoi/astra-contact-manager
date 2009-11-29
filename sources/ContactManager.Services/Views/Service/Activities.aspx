<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<ContactManager.Models.ClientInServices>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table class="data-table" cellpadding="0" cellspacing="0">
        <thead>
            <tr>
                <th>
                    Client
                </th>
                <th>
                    Service
                </th>
                <th>
                    Modify User
                </th>
                <th>
                    Cost
                </th>
                <th>
                    StartDate
                </th>
            </tr>
        </thead>
        <tbody>
            <% foreach (var item in Model)
               { %>
            <tr>
                <td>
                    <a href='<%= Url.Action("Edit", "User", new {id=item.ClientId}) %>'>
                        <%= Html.Encode(item.ClientName)%></a>
                </td>
                <td>
                    <a href='<%= Url.Action("Edit", "Service", new {id=item.ServiceId}) %>'>
                        <%= Html.Encode(item.ServiceName)%></a>
                </td>
                <td>
                    <a href='<%= Url.Action("Edit", "User", new {id=item.UserId}) %>'>
                        <%= Html.Encode(item.UserName)%></a>
                </td>
                <td>
                    <%= Html.Encode(String.Format("{0:F}", item.astra_ServicesReference.Value.Cost)) %>
                </td>
                <td>
                    <%= Html.Encode(item.Date.ToShortDateString())%>
                </td>
            </tr>
            <% } %>
        </tbody>
    </table>
    <p>
        <%=Html.ActionLink("Back to List", "Index") %>
    </p>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
