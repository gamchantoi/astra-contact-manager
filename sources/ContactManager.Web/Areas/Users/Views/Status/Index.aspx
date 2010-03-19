<%@ Page Title="" Language="C#"  Inherits="System.Web.Mvc.ViewPage<IEnumerable<ContactManager.Models.Status>>" %>
<%@ Import Namespace="ContactManager.Web.Helpers"%>

    <table class="data-table" cellpadding="0" cellspacing="0">
        <tr>
            <th>
                Name
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%= Html.JSLink(item.DisplayName, "ShowStatusDialog", Url.Content("~/Users/Status/Edit/")+item.StatusId)%>
                <%--<%= Html.ActionLink(item.DisplayName, "Edit", new { id = item.StatusId })%>--%>
            </td>
        </tr>
    
    <% } %>

    </table>

