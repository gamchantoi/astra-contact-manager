<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<ContactManager.Models.PaymentMethod>>" %>
<%@ Import Namespace="ContactManager.Web.Helpers"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table class="data-table" cellpadding="0" cellspacing="0">
        <tr>
            <th>
                 <%= Html.Resource("Accounts_Resources, Accounts_View_PaymentsMethods_Create_Name")%>
            </th>
            <th>
                <%= Html.Resource("Accounts_Resources, Accounts_View_PaymentsMethods_Create_Comment")%>
            </th>
            <th>
                <%= Html.Resource("Accounts_Resources, Accounts_View_PaymentsMethods_Create_Visible")%>
            </th>
        </tr>
        <% foreach (var item in Model)
           { %>
        <tr>
            <td>
                <%= Html.ActionLink(item.Name, "Edit", new { id = item.MethodId })%>
            </td>
            <td>
                <%= Html.Encode(item.Comment) %>
            </td>
            <td>
                <%= Html.Encode(item.Visible) %>
            </td>
        </tr>
        <% } %>
    </table>
    <p>
        <%= Html.ActionLink(Html.Resource("Accounts_Resources, Accounts_View_PaymentsMethods_Index_CreateNew"), "Create") %>
    </p>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
