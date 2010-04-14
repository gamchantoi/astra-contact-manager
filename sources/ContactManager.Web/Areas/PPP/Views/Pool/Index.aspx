<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<ContactManager.Models.Pool>>" %>

<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="ContactManager.Web.Helpers" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">
        $(document).ready(function() {
            if(<%=Model.Count() %> > 0)
            {
                $('#grid').dataTable({
                    "iDisplayLength": 10,
                    "aaSorting": [[1, "asc"]],
                    "aoColumns": [{ "bSortable": false }, null, null, null]
                });
            }
            $("#grid thead").addClass("ui-widget-header");
        });
    </script>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%= Html.ValidationSummary(Html.Resource("PPP_Resources, PPP_View_Pool_Index_ValidationSummary"), new { @class = "ui-state-error ui-corner-all" })%>
    <div id="container">
        <% Html.Grid(Model)
           .Columns(column =>
           {
               column.For(
                   c =>
                       Html.JSIconLink("Delete", "window.location", Url.Content("~/PPP/Pool/Delete/") + c.PoolId, "ui-icon-trash")).DoNotEncode();
               column.For(c => c.Name + Html.JSIconLink("Edit", "ShowDialog", Url.Content("~/PPP/Pool/Edit/") + c.PoolId, "ui-icon-wrench")).DoNotEncode()
                   .Named(Html.Resource("PPP_Resources, PPP_View_Pool_Index_Name"));
               column.For(c => c.Addresses).Named(Html.Resource("PPP_Resources, PPP_View_Pool_Index_Addresses"));
               column.For(c => c.NextPoolName).Named(Html.Resource("PPP_Resources, PPP_View_Pool_Index_NextPool"));

           }).Attributes(id => "grid").Render();
        %>
    </div>
    <div>
    <p>
        <%= Html.JSLink(Html.Resource("PPP_Resources, PPP_View_Pool_Index_CreateNew"), "ShowPoolDialog",  Url.Content("~/PPP/Pool/Create") )%>
        |
        <%= Html.ActionLink(Html.Resource("PPP_Resources, PPP_View_Pool_Index_DeleteAll"), "DeleteAll")%>
    </p>
    </div>
</asp:Content>
