<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<ContactManager.Models.AccountTransaction>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <% var isAdmin = ViewData["IsAdmin"].ToString().Equals("True"); %>
    <table class="data-table" cellpadding="0" cellspacing="0">
        <thead>
            <tr>
                <% if (isAdmin)
                   { %>
                <th>
                    UserName
                </th>
                <%} %>
                <th>
                    Sum
                </th>
                <th>
                    Comment
                </th>
                <th>
                    Balance
                </th>
                <th>
                    Method
                </th>
                <% if (isAdmin)
                   { %>
                <th>
                    LoadedUserName
                </th>
                <%} %>
                <th>
                    Date
                </th>
            </tr>
        </thead>
        <tbody>
            <% foreach (var item in Model)
               { %>
            <tr>
                <% if (isAdmin)
                   { %>
                <td>
                    <a href='<%= Url.Action("Edit", "User", new {id=item.astra_ClientsReference.Value.UserId}) %>'>
                        <%= Html.Encode(item.astra_ClientsReference.Value.UserName)%></a>
                </td>
                <%} %>
                <td>
                    <%= Html.Encode(String.Format("{0:F}", item.Sum)) %>
                </td>
                <td>
                    <%= Html.Encode(item.Comment) %>
                </td>
                <td>
                    <%= Html.Encode(String.Format("{0:F}", item.Balance)) %>
                </td>
                <td>
                    <%= Html.Encode(item.astra_Account_Transactions_MethodsReference.Value.Name) %>
                <%if (item.astra_Account_Transactions_MethodsReference.Value.Name.Equals("Service"))
                  { %>
                  <a href='<%= Url.Action("Edit", "Service", new {id=item.astra_ServicesReference.Value.ServiceId}) %>'>
                        <%= Html.Encode(item.astra_ServicesReference.Value.Name)%></a>
                  
                    <%} %>
                    <%if (item.astra_Account_Transactions_MethodsReference.Value.Name.Equals("Profile"))
                  { %>
                  <a href='<%= Url.Action("Edit", "Profile", new {id=item.mkt_PPPProfilesReference.Value.ProfileId}) %>'>
                        <%= Html.Encode(item.mkt_PPPProfilesReference.Value.Name)%></a>
                    <%} %>
                </td>
                <% if (isAdmin)
                   { %>
                <td>
                    <%if (item.aspnet_UsersReference.Value == null || item.aspnet_UsersReference.Value.UserName.Equals("System"))
                      { %>
                    SYSTEM
                    <%}
                      else
                      {%>
                    <a href='<%= Url.Action("Edit", "User", new {id=item.aspnet_UsersReference.Value.UserId}) %>'>
                        <%= Html.Encode(item.aspnet_UsersReference.Value.UserName)%></a>
                    <%} %>
                </td>
                <%} %>
                <td>
                    <%= Html.Encode(String.Format("{0:g}", item.Date)) %>
                </td>
            </tr>
            <% } %>
        </tbody>
    </table>
    <% if (isAdmin)
       { %>
    <p>
        <%= Html.ActionLink("Create New Transaction Method", "CreateTransactionMethod")%>
    </p>
    <div style='text-align: right;'>
        <%= Html.ActionLink("Process Payment", "ProcessClientPayment")%>
    </div>
    <% } %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
