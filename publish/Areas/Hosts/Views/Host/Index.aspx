<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<ContactManager.Models.Host>>" %>
<%@ Import Namespace="ContactManager.Web.Helpers"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table class="data-table" cellpadding="0" cellspacing="0">
        <thead>
            <tr>
                <th>
                    <%= Html.Resource("Hosts_Resources, Hosts_View_Index_Select")%>
                </th>
                <th>
                    <%= Html.Resource("Hosts_Resources, Hosts_View_Index_Delete")%>
                </th>
                <th>
                    <%= Html.Resource("Hosts_Resources, Hosts_View_Index_Address")%>
                </th>
                <th>
                    <%= Html.Resource("Hosts_Resources, Hosts_View_Index_UserName")%>
                </th>
                <th>
                    <%= Html.Resource("Hosts_Resources, Hosts_View_Index_UserPassword")%>
                </th>
                <th>
                    <%= Html.Resource("Hosts_Resources, Hosts_View_Index_LastUpdatedDate")%>
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
                    <%= Html.Resource("Hosts_Resources, Hosts_View_Index_Selected")%>
                    <% }
                       else
                       {%>
                    <%= Html.ActionLink(Html.Resource("Hosts_Resources, Hosts_View_Index_Select"), "Select", new { id = item.HostId })%>
                    <%} %>
                </td>
                <td>
                    <a href='<%= Url.Action("Delete", new {id=item.HostId}) %>'>
                        <%= Html.Resource("Hosts_Resources, Hosts_View_Index_Delete")%></a>
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
        <%= Html.ActionLink(Html.Resource("Hosts_Resources, Hosts_View_Index_Create"), "Create")%>
    </p>
</asp:Content>
