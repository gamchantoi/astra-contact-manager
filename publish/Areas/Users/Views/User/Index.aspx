<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ContactManager.Users.ViewModels.ListClientViewModel>" %>

<%@ Import Namespace="ContactManager.Web.Helpers" %>
<%@ Import Namespace="MvcContrib" %>
<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="MvcContrib.UI.Grid.ActionSyntax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">
        $(document).ready(function() {
            if('<%=Model.Clients.Count() %>' > 0)
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
               column.For(
                   c =>
                       Html.JSIconLink("Load", "ShowDialog", Url.Content("~/Users/User/Load/") + c.UserId, "ui-icon-circle-plus")).DoNotEncode();
               column.For(c => c.UserName + Html.JSIconLink("Edit", "window.location", Url.Content("~/Users/User/Edit/") + c.UserId, "ui-icon-person")).DoNotEncode()
                   .Named(Html.Resource("Users_Resources, Users_User_Index_UserName"));
               column.For(c => c.Role).Named(Html.Resource("Users_Resources, Users_User_Index_Role"));
               column.For(c => c.FullName).Named(Html.Resource("Users_Resources, Users_User_Index_FullName"));
               column.For(c => c.ProfileName).Named(Html.Resource("Users_Resources, Users_User_Index_ProfileName"));
               column.For(c => Html.JSLink(Html.Resource("Users_Resources, Users_User_Index_View"), "ShowDialog", Url.Content("~/Services/Service/ClientServices/" + c.UserId))).DoNotEncode()
                   .Named(Html.Resource("Users_Resources, Users_User_Index_Services"));
               column.For(c => c.Balance.ToString("C")).Named(string.Format("{0} ({1})", Html.Resource("Users_Resources, Users_User_Index_Balance"), Html.Encode(Model.TotalBalance.ToString("C"))));
               column.For(c => c.StatusDisplayName).Named(Html.Resource("Users_Resources, Users_User_Index_Status"));

           }).Attributes(id => "grid").Render();
        %>
        <%= Html.JSLink(Html.Resource("Users_Resources, Users_User_Index_View"), "ShowDialog",
                           Url.Content("~/Services/Service/ClientServices/"))%>
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
