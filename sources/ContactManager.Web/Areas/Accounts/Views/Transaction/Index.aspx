<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<ContactManager.Accounts.ViewModels.TransactionVewModel>>" %>
<%@ Import Namespace="ContactManager.Web.Helpers"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table class="data-table" cellpadding="0" cellspacing="0">
        <tr>
            <th>
                 <%= Html.Resource("Accounts_Resources, Accounts_View_Index_ClientName")%>
            </th>
            <th>
                <%= Html.Resource("Accounts_Resources, Accounts_View_Index_Sum")%>
            </th>
            <th>
                <%= Html.Resource("Accounts_Resources, Accounts_View_Index_Balance")%>
            </th>
            <th>
                <%= Html.Resource("Accounts_Resources, Accounts_View_Index_Comment")%>
            </th>
            <th>
                <%= Html.Resource("Accounts_Resources, Accounts_View_Index_UserName")%>
            </th>
            <th>
                <%= Html.Resource("Accounts_Resources, Accounts_View_Index_TransactionName")%>
            </th>
            <th>
                <%= Html.Resource("Accounts_Resources, Accounts_View_Index_Date")%>
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%= Html.Encode(item.ClientUserName) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:F}", item.Sum)) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:F}", item.Balance)) %>
            </td>
            <td>
                <%= Html.Encode(item.Comment) %>
            </td>
            <td>
                <%= Html.Encode(item.UserUserName) %>
            </td>
            <td>
                <%= Html.Encode(item.TransactionName) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:g}", item.Date)) %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%= Html.ActionLink(Html.Resource("Accounts_Resources, Accounts_View_Index_CreateNew"), "Create")%>&nbsp;|&nbsp;
        <%= Html.ActionLink(Html.Resource("Accounts_Resources, Accounts_View_Index_PaymentsMethods"), "Index", "PaymentMethod")%>
    </p>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>

