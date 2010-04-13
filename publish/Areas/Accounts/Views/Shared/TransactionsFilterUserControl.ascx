<%@ Import Namespace="ContactManager.Web.Helpers" %>
<%@ Import Namespace="ContactManager.Accounts.Controllers" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ContactManager.Accounts.ViewModels.TransactionViewModel>" %>
<% using (Html.BeginForm("FiltredList", "Transaction", new { @area = "Accounts" }))
   {%>
<div>

    <script type="text/javascript">
        $(document).ready(function() {
        $("#PaymentMethods").dropdownchecklist();
        });
    </script>

    <table class="data-table">
        <tbody>
            <tr>
                <td>
                    <%= Html.Label(Html.Resource("Accounts_Resources, Accounts_Shared_TransactionsFilterUserControl_Years") + ":")%>
                    <%= Html.DropDownList("Years", Model.Filter.YearsList) %>
                </td>
                <td>
                    <%= Html.Label(Html.Resource("Accounts_Resources, Accounts_Shared_TransactionsFilterUserControl_Months"))%>
                    <%= Html.DropDownList("Months", Model.Filter.MonthsList) %>
                </td>
                <td>
                    <%= Html.Label(Html.Resource("Accounts_Resources, Accounts_Shared_TransactionsFilterUserControl_PaymentMethods"))%>
                    <%--<%= Html.DropDownList("PaymentMethods", Model.Filter.PaymentMethodsList) %>--%>
                    <%= Html.DropDownCheckBox("PaymentMethods",Model.Filter.PaymentMethodsList) %>
                </td>
                <td>
                    <input type="submit" value="<%= Html.Resource("Accounts_Resources, Accounts_Shared_TransactionsFilterUserControl_Select")%>" />
                </td>
            </tr>
        </tbody>
    </table>
</div>
<% } %>