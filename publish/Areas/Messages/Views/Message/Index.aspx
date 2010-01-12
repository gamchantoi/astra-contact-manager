<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<ContactManager.Models.Message>>" %>
<%@ Import Namespace="ContactManager.Web.Helpers"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table class="data-table" cellpadding="0" cellspacing="0">
        <tr>
            <th>
            </th>
            <th>
                <%= Html.Resource("Messages_Resources, Messages_View_Index_Title")%>
            </th>
            <th>
                <%= Html.Resource("Messages_Resources, Messages_View_Index_Text")%>
            </th>
            <th>
                <%= Html.Resource("Messages_Resources, Messages_View_Index_Date")%>
            </th>
        </tr>
        <% foreach (var item in Model)
           { %>
        <tr>
            <td>
                <%= Html.ActionLink(Html.Resource("Messages_Resources, Messages_View_Index_Details"), "Details", new { id = item.MessageId })%>
                |
                <%= Html.ActionLink(Html.Resource("Messages_Resources, Messages_View_Index_Delete"), "Delete", new { id = item.MessageId })%>
            </td>
            <td>
                <%= Html.Encode(item.Title) %>
            </td>
            <td>
                <%= Html.Encode(item.Text) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:g}", item.Date)) %>
            </td>
        </tr>
        <% } %>
    </table>
    <p>
        <%= Html.ActionLink(Html.Resource("Messages_Resources, Messages_View_Index_CreateNew"), "Create") %>&nbsp;|&nbsp;
        <%= Html.ActionLink(Html.Resource("Messages_Resources, Messages_View_Index_CreateMessageType"), "Index", "MessageType")%>
    </p>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
