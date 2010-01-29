<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ContactManager.Users.ViewModels.ListClientViewModel>" %>

<%@ Import Namespace="ContactManager.Web.Helpers" %>
<%@ Import Namespace="MvcContrib" %>
<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="MvcContrib.UI.Grid.ActionSyntax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">
        $(document).ready(function() {
            if(<%=Model.Clients.Count() %> > 0)
            {
                jQuery('#grid').dataTable({
                    "iDisplayLength": 10,
                    "aaSorting": [[1, "asc"]],
                    "aoColumns": [{ "bSortable": false }, null, null, null, null, { "bSortable": false }, null, null]
                });
            }
            $("#grid thead").addClass("ui-widget-header");
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%= Html.ValidationSummary(Html.Resource("Users_Resources, Users_User_Index_ValidationSummary"), new { @class = "ui-state-error ui-corner-all" })%>
    <div id="container">
        <% Html.Grid(Model.Clients)
           .Columns(column =>
           {
               //column.For(c => Html.ActionLink(Html.Resource("Users_Resources, Users_User_Index_Load"), "Load", new { id = c.UserId })).DoNotEncode();
               column.For(
                   c =>
                       Html.JSIconLink("Load", "ShowDialog", Url.Content("~/Users/User/Load/") + c.UserId, "ui-icon-circle-plus")).DoNotEncode();
               column.For(c => c.UserName + "<span class='ui-icon-click ui-icon ui-icon-person' onclick=\"javascript:window.location='" + Url.Content("~/Users/User/Edit/") + c.UserId + "';\"></span>").DoNotEncode()
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
    <div>
        <p>
            <% if (!Model.Deleted)
               {%>
            <%= Html.ActionLink(Html.Resource("Users_Resources, Users_User_Index_CreateNew"), "Create")%>
            <%} %>
        </p>
    </div>
</asp:Content>
