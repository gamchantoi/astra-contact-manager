<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<ContactManager.Models.Host>>" %>

<%@ Import Namespace="ContactManager.Web.Helpers" %>
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
                <%=Html.JSIconLink("", "", "", "ui-icon-check") %>
                <% }
                   else
                   {%>
                <%= Html.JSIconLink("Edit", "ChangeHost", Url.Content("~/Hosts/Host/Select/") + item.HostId, "ui-icon-minus")%>
                <%--<%= Html.JSIconLink("Edit", "window.location", Url.Content("~/Hosts/Host/Select/") + item.HostId, "ui-icon-minus")%>--%>
                <%} %>
            </td>
            <td>
                <a href='<%= Url.Action("Delete", new {id=item.HostId}) %>'>
                    <%= Html.Resource("Hosts_Resources, Hosts_View_Index_Delete")%></a>
            </td>
            <td>
                <%= Html.JSLink(item.Address, "ShowHostDialog", Url.Content("~/Hosts/Host/Edit/" + item.HostId))%>
                <%--                <a href='<%= Url.Action("Edit", new {id=item.HostId}) %>'>
                    <%= Html.Encode(item.Address) %></a>--%>
            </td>
            <td>
                <%= Html.Encode(item.UserName) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:g}", item.LastUpdatedDate)) %>
            </td>
        </tr>
        <% } %>
    </tbody>
</table>
<p>
    <%--<%= Html.ActionLink(Html.Resource("Hosts_Resources, Hosts_View_Index_Create"), "Create")%>--%>
    <%= Html.JSLink(Html.Resource("Hosts_Resources, Hosts_View_Index_Create"), "ShowHostDialog", Url.Content("~/Hosts/Host/Create"))%>
</p>
