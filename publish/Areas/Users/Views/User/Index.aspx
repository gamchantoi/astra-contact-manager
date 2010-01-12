<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ContactManager.Users.ViewModels.ListClientViewModel>" %>
<%@ Import Namespace="ContactManager.Web.Helpers"%>

<%@ Import Namespace="MvcContrib" %>
<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="MvcContrib.UI.Grid.ActionSyntax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="<%= Url.Content("~/media/css/Grid.css")%>" rel="stylesheet" type="text/css" />
    <script src="<%= Url.Content("~/media/js/jquery.js")%>" type="text/javascript"></script>
    <script src="<%= Url.Content("~/media/js/jquery.dataTables.js")%>" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        $(document).ready(function() {
            $('#grid').dataTable({
                "iDisplayLength": 25,
                "aaSorting": [[1, "asc"]],
                "aoColumns": [{ "bSortable": false }, null, null, null, null, { "bSortable": false }, null, null]
            });
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%= Html.ValidationSummary(Html.Resource("Users_Resources, Users_User_Index_ValidationSummary"))%>
    <div id="container">
        <% Html.Grid(Model.Clients)
           .Columns(column =>
           {
               column.For(c => Html.ActionLink(Html.Resource("Users_Resources, Users_User_Index_Load"), "Load", new { id = c.UserId })).DoNotEncode();
               column.For(c => Html.ActionLink(c.UserName, "Edit", new { id = c.UserId })).DoNotEncode()
                   .Named(Html.Resource("Users_Resources, Users_User_Index_UserName"));
               column.For(c => c.Role).Named(Html.Resource("Users_Resources, Users_User_Index_Role"));
               column.For(c => c.FullName).Named(Html.Resource("Users_Resources, Users_User_Index_FullName"));
               column.For(c => c.ProfileName).Named(Html.Resource("Users_Resources, Users_User_Index_ProfileName"));
               column.For(c => Html.ActionLink(Html.Resource("Users_Resources, Users_User_Index_View"), "ClientServices", "Service", new { area = "Services", id = c.UserId }, null)).DoNotEncode()
                   .Named(Html.Resource("Users_Resources, Users_User_Index_Services"));
               column.For(c => c.Balance).Named(Html.Resource("Users_Resources, Users_User_Index_Balance") + Html.Encode(String.Format("({0:F})", Model.TotalBalance)));
               column.For(c => c.StatusDisplayName).Named(Html.Resource("Users_Resources, Users_User_Index_Status"));

           }).Attributes(id => "grid").Render();
        %>
    </div>
    <p>
        <% if (!Model.Deleted)
           {%>
        <%= Html.ActionLink(Html.Resource("Users_Resources, Users_User_Index_CreateNew"), "Create")%>&nbsp;|&nbsp;
        <%= Html.ActionLink(Html.Resource("Users_Resources, Users_User_Index_DeleteAllUsers"), "DeleteAll")%>&nbsp;|&nbsp;
        <%= Html.ActionLink(Html.Resource("Users_Resources, Users_User_Index_Statuses"), "Index", "Status")%>&nbsp;|&nbsp;
        <%= Html.ActionLink(Html.Resource("Users_Resources, Users_User_Index_Addresses"), "Index", "Address", new { area = "Addresses" }, null)%>
        <%} %>
    </p>
    <div style="text-align: right;">
        <%= Html.ActionLink("Clear database", "ClearAllData")%>
    </div>
</asp:Content>
