<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<ContactManager.Models.ActiveConnections>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table class="data-table" cellpadding="0" cellspacing="0">
        <thead>
            <tr>
                <th>
                    Name
                </th>
                <th>
                    Service
                </th>
                <th>
                    CallerId
                </th>
                <th>
                    Address
                </th>
                <th>
                    Uptime
                </th>
                <th>
                    Encoding
                </th>
                <th>
                    SessionId
                </th>
                <th>
                    LimitBytesIn
                </th>
                <th>
                    LimitBytesOut
                </th>
            </tr>
        </thead>
        <tbody>
            <% foreach (var item in Model)
               { %>
            <tr>
                <td>
                    <%= Html.Encode(item.Name) %>
                </td>
                <td>
                    <%= Html.Encode(item.Service) %>
                </td>
                <td>
                    <%= Html.Encode(item.CallerId) %>
                </td>
                <td>
                    <%= Html.Encode(item.Address) %>
                </td>
                <td>
                    <%= Html.Encode(item.Uptime) %>
                </td>
                <td>
                    <%= Html.Encode(item.Encoding) %>
                </td>
                <td>
                    <%= Html.Encode(item.SessionId) %>
                </td>
                <td>
                    <%= Html.Encode(item.LimitBytesIn) %>
                </td>
                <td>
                    <%= Html.Encode(item.LimitBytesOut) %>
                </td>
            </tr>
            <% } %>
        </tbody>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
<script src="Scripts/StyleSetter.js" type="text/javascript"></script>
</asp:Content>
