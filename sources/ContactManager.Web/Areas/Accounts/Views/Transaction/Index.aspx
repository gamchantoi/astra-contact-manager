<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<ContactManager.Accounts.ViewModels.TransactionVewModel>>" %>

<%@ Import Namespace="ContactManager.Web.Helpers" %>
<%@ Import Namespace="MvcContrib" %>
<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="MvcContrib.UI.Grid.ActionSyntax" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="<%= Url.Content("~/media/css/Grid.css")%>" rel="stylesheet" type="text/css" />

    <script src="<%= Url.Content("~/media/js/jquery.dataTables.js")%>" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        $(document).ready(function() {
            $('#grid').dataTable({
                "iDisplayLength": 10,
                "aaSorting": [[1, "asc"]],
                "aoColumns": [null, null, null, null, null, null, null]
            });
        });
    </script>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <div id="container">
        <% Html.Grid(Model)
           .Columns(column =>
           {
               column.For(t => t.ClientUserName).Named(Html.Resource("Accounts_Resources, Accounts_View_Index_ClientName"));
               column.For(t => t.Sum).Named(Html.Resource("Accounts_Resources, Accounts_View_Index_Sum"));
               column.For(t => t.Balance).Named(Html.Resource("Accounts_Resources, Accounts_View_Index_Balance"));
               column.For(t => t.Comment).Named(Html.Resource("Accounts_Resources, Accounts_View_Index_Comment"));
               column.For(t => t.UserUserName).Named(Html.Resource("Accounts_Resources, Accounts_View_Index_UserName"));
               column.For(t => t.TransactionName).Named(Html.Resource("Accounts_Resources, Accounts_View_Index_TransactionName"));
               column.For(t => t.Date).Named(Html.Resource("Accounts_Resources, Accounts_View_Index_Date"));

           }).Attributes(id => "grid").Render();
        %>
    </div>
    <%--<table class="data-table" cellpadding="0" cellspacing="0">
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
        <% foreach (var item in Model)
           { %>
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
    </table>--%>
    <p>
        <%= Html.ActionLink(Html.Resource("Accounts_Resources, Accounts_View_Index_CreateNew"), "Create")%>&nbsp;|&nbsp;
        <%= Html.ActionLink(Html.Resource("Accounts_Resources, Accounts_View_Index_PaymentsMethods"), "Index", "PaymentMethod")%>
    </p>
</asp:Content>
