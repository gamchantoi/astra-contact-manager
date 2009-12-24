<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ContactManager.Users.ViewModels.ListClientViewModel>" %>
<%@ Import Namespace="MvcContrib"%>

<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="MvcContrib.UI.Grid.ActionSyntax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<script src="Scripts/PaggingGrid.js" type="text/javascript"></script>--%>
    <link href="<%= Url.Content("~/media/css/Grid.css")%>" rel="stylesheet" type="text/css" />

    <script src="<%= Url.Content("~/media/js/jquery.js")%>" type="text/javascript"></script>

    <script src="<%= Url.Content("~/media/js/jquery.dataTables.js")%>" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        $(document).ready(function() {
            $('#grid').dataTable({
                "iDisplayLength": 25,
                "aaSorting": [[1, "asc"]],
                "aoColumns": [{ "bSortable": false }, null, null, null, null, { "bSortable": false}, null, null]
            });
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%=Html.Encode(ViewData["Message"])%>
    <%= Html.ValidationSummary("Operation was unsuccessful. Please view details and try again.")%>
    <div id="container">
        <% Html.Grid(Model.Clients)
           .Columns(column =>
           {
               column.For(c => Html.ActionLink("Load", "Load", new {id = c.UserId})).DoNotEncode();
               column.For(c => Html.ActionLink(c.UserName, "Edit", new { id = c.UserId })).DoNotEncode()
                   .Named("UserName");
               column.For(c => c.Role);
               column.For(c => c.FullName);
               column.For(c => c.ProfileName);
               column.For(c => Html.ActionLink("View", "ClientServices", "Service", new { area = "Services", id = c.UserId }, null)).DoNotEncode()
                   .Named("Services");
               column.For(c => c.Balance).Named("Balance " + Html.Encode(String.Format("({0:F})", Model.TotalBalance)));
               column.For(c => c.StatusDisplayName).Named("Status");

           }).Attributes(id => "grid").Render();
        %>
    </div>
    <%--    <table class="data-table" cellpadding="0" cellspacing="0">
        <thead>
            <tr>
                <th style="width: 40px;">
                    <a href='<%= Url.Action("Index", new {deleted=!Model.Deleted}) %>'>
                        <%=Html.Encode(Model.Deleted ? "Active" : "Deleted")%></a>
                </th>
                <th style="width: 40px;">
                    Money
                </th>
                <th>
                    UserName(<%=Html.Encode(Model.TotalUsers)%>)
                </th>
                <th>
                    FullName
                </th>
                <th>
                    Tariff
                </th>
                <th>
                    Services
                </th>
                <th>
                    Balance(<%=Html.Encode(String.Format("{0:F}", Model.TotalBalance))%>)
                </th>
                <th style="width: 40px;">
                    Status
                </th>
            </tr>
        </thead>
        <tbody>
            <% foreach (var item in Model.Clients)
               { %>
            <tr class="<%= Html.Encode(item.StatusName)%> <%= Html.Encode(item.Role) %>">
                <td>
                    <a href='<%= Url.Action("Status", new {id=item.UserId, status=Model.Deleted}) %>'>
                        <%=Html.Encode(Model.Deleted ? "Recover" : "Delete")%></a>
                </td>
                <td>
                    <a href='<%= Url.Action("Load", new {id=item.UserId}) %>'>Load</a>
                </td>
                <td>
                    <a href='<%= Url.Action("Edit", new {id=item.UserId}) %>'>
                        <%= Html.Encode(item.UserName) %></a>
                </td>
                <td>
                    <%= Html.Encode(item.FullName)%>
                </td>
                <td>
                    <%= Html.Encode(item.ProfileName)%>
                </td>
                <td>
                    <%= Html.ActionLink("View", "ClientServices", "Service", new {area="Services", id=item.UserId}, null)%>
                </td>
                <td>
                    <%= Html.Encode(String.Format("{0:F}", item.Balance)) %>
                </td>
                <td>
                    <%= Html.Encode(item.StatusDisplayName)%>
                </td>
            </tr>
            <%} %>
        </tbody>
    </table>--%>
    <%--<div id="topic-grid"></div>--%>
    <p>
        <% if (!Model.Deleted)
           {%>
        <%= Html.ActionLink("Create New", "Create")%>&nbsp;|&nbsp;
        <%= Html.ActionLink("Delete All Users", "DeleteAll")%>&nbsp;|&nbsp;
        <%= Html.ActionLink("Statuses", "Index", "Status")%>&nbsp;|&nbsp;
        <%= Html.ActionLink("Addresses", "Index", "Address", new { area = "Addresses" }, null)%>
        <%} %>
    </p>
    <div style='text-align: right;'>
        <%= Html.ActionLink("Clear database", "ClearAllData")%>
    </div>
</asp:Content>
