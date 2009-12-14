<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<ContactManager.Accounts.ViewModels.TransactionVewModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table class="data-table" cellpadding="0" cellspacing="0">
        <tr>
            <th>
                ClientName
            </th>
            <th>
                Sum
            </th>
            <th>
                Balance
            </th>
            <th>
                Comment
            </th>
            <th>
                UserName
            </th>
            <th>
                TransactionName
            </th>
            <th>
                Date
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%= Html.Encode(item.ClientUserName) %>
            </td>
            <td>
                <%= Html.Encode(item.Sum) %>
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
        <%= Html.ActionLink("Create New", "Create") %>&nbsp;|&nbsp;
        <%= Html.ActionLink("Payments Methods", "Index", "PaymentMethod")%>
    </p>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>

