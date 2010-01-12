<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<ContactManager.PPP.Models.ActiveConnections>>" %>
<%@ Import Namespace="ContactManager.Web.Helpers"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table  class="data-table" cellpadding="0" cellspacing="0">
        <tr>
            <th></th>
            <th>
                <%= Html.Resource("PPP_Resources, PPP_View_ActiveConnections_Index_Name")%>
            </th>
            <th>
                <%= Html.Resource("PPP_Resources, PPP_View_ActiveConnections_Index_Service")%>
            </th>
            <th>
                <%= Html.Resource("PPP_Resources, PPP_View_ActiveConnections_Index_CallerId")%>
            </th>
            <th>
                <%= Html.Resource("PPP_Resources, PPP_View_ActiveConnections_Index_Address")%>
            </th>
            <th>
                <%= Html.Resource("PPP_Resources, PPP_View_ActiveConnections_Index_Uptime")%>
            </th>
            <th>
                <%= Html.Resource("PPP_Resources, PPP_View_ActiveConnections_Index_Encoding")%>
            </th>
            <th>
                <%= Html.Resource("PPP_Resources, PPP_View_ActiveConnections_Index_SessionId")%>
            </th>
            <th>
                <%= Html.Resource("PPP_Resources, PPP_View_ActiveConnections_Index_LimitBytesIn")%>
            </th>
            <th>
                <%= Html.Resource("PPP_Resources, PPP_View_ActiveConnections_Index_LimitBytesOut")%>
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
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

    </table>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>

