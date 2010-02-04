<%@ Import Namespace="ContactManager.Web.Helpers" %>
<%@ Import Namespace="ContactManager.Accounts.Controllers" %>
<%--<%@ Import Namespace="ContactManager.Web.Helpers" %>
<%@ Import Namespace="ContactManager.Users.ViewModels" %>--%>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ContactManager.Accounts.ViewModels.TransactionViewModel>" %>
<% using (Html.BeginForm("FiltredList", "Transaction", new { @area = "Accounts" }))
   {%>
<div>
    <%= Html.Label(Html.Resource("Accounts_Resources, Accounts_Shared_TransactionsFilterUserControl_Years"))%>
    <%= Html.DropDownList("Years", Model.Filter.YearsList) %>
    <%= Html.Label(Html.Resource("Accounts_Resources, Accounts_Shared_TransactionsFilterUserControl_Months"))%>
    <%= Html.DropDownList("Months", Model.Filter.MonthsList) %>
    <%= Html.Label(Html.Resource("Accounts_Resources, Accounts_Shared_TransactionsFilterUserControl_PaymentMethods"))%>
    <%= Html.DropDownList("PaymentMethods", Model.Filter.PaymentMethodsList) %>
    <input type="submit" value=<%= Html.Resource("Accounts_Resources, Accounts_Shared_TransactionsFilterUserControl_Select")%> />
</div>
<% } %>