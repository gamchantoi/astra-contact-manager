<%@ Import Namespace="ContactManager.Web.Helpers" %>
<%@ Import Namespace="ContactManager.Accounts.Controllers" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ContactManager.Accounts.TransactionsFilter>" %>
<% using (Html.BeginForm("FiltredList", "Transaction", new { @area = "Accounts" }))
   {%>
<div>

    <script type="text/javascript">
        var payment_methods_id = '#' + '<%=Model.PaymentMethodsList.Name %>';
        
        $(document).ready(function() {
            $(payment_methods_id).dropdownchecklist();
        });
    </script>

    <table class="data-table">
        <tbody>
            <tr>
                <td>
                    <%= Html.Label(Html.Resource("Accounts_Resources, Accounts_Shared_TransactionsFilterUserControl_Years") + ":")%>
                    <%= Html.DropDownList("Years", Model.YearsList) %>
                </td>
                <td>
                    <%= Html.Label(Html.Resource("Accounts_Resources, Accounts_Shared_TransactionsFilterUserControl_Months"))%>
                    <%= Html.DropDownList("Months", Model.MonthsList) %>
                </td>
                <td>
                    <%= Html.Label(Html.Resource("Accounts_Resources, Accounts_Shared_TransactionsFilterUserControl_PaymentMethods"))%>
                    <%Html.RenderPartial("DropDownCheckBoxUserControl", Model.PaymentMethodsList); %>
                </td>
                <td>
                    <input type="submit" value="<%= Html.Resource("Accounts_Resources, Accounts_Shared_TransactionsFilterUserControl_Select")%>" />
                </td>
            </tr>
        </tbody>
    </table>
</div>
<% } %>