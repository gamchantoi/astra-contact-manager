<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<ContactManager.PPP.Models.ActiveConnections>>" %>

<%@ Import Namespace="ContactManager.Web.Helpers" %>
<%@ Import Namespace="MvcContrib" %>
<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="MvcContrib.UI.Grid.ActionSyntax" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">
        $(document).ready(function() {
            if(<%=Model.Count() %> > 0)
            {
                jQuery('#grid').dataTable({
                    "iDisplayLength": 10,
                    "aaSorting": [[0, "asc"]],
                    "aoColumns": [null, null, null, null, null, null, null, null, null]
                });
            }
            $("#grid thead").addClass("ui-widget-header");
        });
    </script>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="container">
        <% Html.Grid(Model)
           .Columns(column =>
           {
               column.For(c => c.Name).Named(Html.Resource("PPP_Resources, PPP_View_ActiveConnections_Index_Name"));
               column.For(c => c.Service).Named(Html.Resource("PPP_Resources, PPP_View_ActiveConnections_Index_Service"));
               column.For(c => c.CallerId).Named(Html.Resource("PPP_Resources, PPP_View_ActiveConnections_Index_CallerId"));
               column.For(c => c.Address).Named(Html.Resource("PPP_Resources, PPP_View_ActiveConnections_Index_Address"));
               column.For(c => c.Uptime).Named(Html.Resource("PPP_Resources, PPP_View_ActiveConnections_Index_Uptime"));
               column.For(c => c.Encoding).Named(Html.Resource("PPP_Resources, PPP_View_ActiveConnections_Index_Encoding"));
               column.For(c => c.SessionId).Named(Html.Resource("PPP_Resources, PPP_View_ActiveConnections_Index_SessionId"));
               column.For(c => c.LimitBytesIn).Named(Html.Resource("PPP_Resources, PPP_View_ActiveConnections_Index_LimitBytesIn"));
               column.For(c => c.LimitBytesOut).Named(Html.Resource("PPP_Resources, PPP_View_ActiveConnections_Index_LimitBytesOut"));

           }).Attributes(id => "grid").Render();
        %>
    </div>
</asp:Content>
