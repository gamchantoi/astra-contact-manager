<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<ContactManager.Models.ClientInServices>>" %>
<%@ Import Namespace="ContactManager.Web.Helpers"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table class="data-table" cellpadding="0" cellspacing="0">
        <thead>
            <tr>
                <th>
                    <%= Html.Resource("Services_Resources, Services_View_Index_Client")%>
                </th>
                <th>
                    <%= Html.Resource("Services_Resources, Services_View_Index_Service")%>
                </th>
                <th>
                    <%= Html.Resource("Services_Resources, Services_View_Index_ModifyUser")%>
                </th>
                <th>
                    <%= Html.Resource("Services_Resources, Services_View_Index_Cost")%>
                </th>
                <th>
                    <%= Html.Resource("Services_Resources, Services_View_Index_StartDate")%>
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
        <%=Html.ActionLink(Html.Resource("Services_Resources, Services_View_Index_BackToList"), "Index") %>
    </p>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
