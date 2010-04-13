<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<ContactManager.Models.MessageType>>" %>

<%@ Import Namespace="ContactManager.Web.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table class="data-table" cellpadding="0" cellspacing="0">
        <tr>
            <th>
            </th>
            <th>
                <%= Html.Resource("Messages_Resources, Messages_View_MessageType_Index_Name")%>
            </th>
        </tr>
        <% foreach (var item in Model)
           { %>
        <tr>
            <td>
                <%= Html.ActionLink(Html.Resource("Messages_Resources, Messages_View_MessageType_Index_Edit"), "Edit", new { id=item.TypeId }) %>
            </td>
            <td>
                <%= Html.Encode(item.Name) %>
            </td>
        </tr>
        <% } %>
    </table>
    <p>
        <%= Html.JSLink(Html.Resource("Messages_Resources, Messages_View_MessageType_Index_CreateNew"), "ShowMessageTypeDialog", Url.Content("~/Messages/MessageType/Create"))%>
        <%--<%= Html.ActionLink(Html.Resource("Messages_Resources, Messages_View_MessageType_Index_CreateNew"), "Create") %>--%>
    </p>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
