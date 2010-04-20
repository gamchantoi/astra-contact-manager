<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<ContactManager.Accounts.ViewModels.TransactionViewModel>" %>

<%@ Import Namespace="ContactManager.Web.Helpers" %>
<%@ Import Namespace="MvcContrib" %>
<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="MvcContrib.UI.Grid.ActionSyntax" %>

    <script language="javascript" type="text/javascript">
        $(document).ready(function() {
            if('<%=Model.Transactions.Count() %>' > 0)
            {
                $('#grid1').dataTable({
                    "iDisplayLength": 10,
                    "aaSorting": [[1, "asc"]],
                    "aoColumns": [null, null, null, null, null, null, null]
                });
            }
            $("#grid1 thead").addClass("ui-widget-header");
        });
    </script>


    <%--<%Html.RenderPartial("TransactionsFilterUserControl", Model); %>--%>
    <div id="container1">
        <% Html.Grid(Model.Transactions)
           .Columns(column =>
           {
               column.For(t => t.ClientUserName).Named(Html.Resource("Accounts_Resources, Accounts_View_Index_ClientName"));
               //column.For(t =>t.ClientUserName +Html.JSIconLink("Index", "window.location",
                                  // Url.Content("~/Accounts/Transaction/ClientTransactions/") + t.ClientUserId,"ui-icon-document")).DoNotEncode();
               column.For(t => t.Sum.ToString("C")).Named(string.Format("{0} ({1})", Html.Resource("Accounts_Resources, Accounts_View_Index_Sum"), Model.TotalSum.ToString("C")));
               column.For(t => t.Balance.ToString("C")).Named(Html.Resource("Accounts_Resources, Accounts_View_Index_Balance"));
               column.For(t => t.UserUserName).Named(Html.Resource("Accounts_Resources, Accounts_View_Index_UserName"));
               column.For(t => t.TransactionName).Named(Html.Resource("Accounts_Resources, Accounts_View_Index_TransactionName"));
               column.For(t => t.Comment).Named(Html.Resource("Accounts_Resources, Accounts_View_Index_Comment"));
               column.For(t => t.Date).Named(Html.Resource("Accounts_Resources, Accounts_View_Index_Date"));
           }).Attributes(id => "grid1").Render();
        %>
    </div>

