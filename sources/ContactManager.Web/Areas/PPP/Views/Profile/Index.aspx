<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<ContactManager.Models.Profile>>" %>

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
                    "aoColumns": [{ "bSortable": false }, null, null, null, null, null]
                });
            }
            $("#grid thead").addClass("ui-widget-header");
        });
    </script>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%= Html.ValidationSummary(Html.Resource("PPP_Resources, PPP_View_Profile_Index_ValidationSummary"), new { @class = "ui-state-error ui-corner-all" })%>
    <div id="container">
        <% Html.Grid(Model)
           .Columns(column =>
           {
               column.For(
                   c =>
                       Html.JSIconLink("Delete", "window.location", Url.Content("~/PPP/Profile/Delete/") + c.ProfileId, "ui-icon-trash")).DoNotEncode();
               column.For(c => c.Name + Html.JSIconLink("Edit", "ShowDialog", Url.Content("~/PPP/Profile/Edit/") + c.PoolId, "ui-icon-wrench")).DoNotEncode()
                   .Named(Html.Resource("PPP_Resources, PPP_View_Pool_Index_Name"));
               column.For(c => c.LocalAddress).Named(Html.Resource("PPP_Resources, PPP_View_Pool_Index_Addresses"));
               column.For(c => c.PoolName).Named(Html.Resource("PPP_Resources, PPP_View_Pool_Index_Addresses"));
               column.For(c => c.RateLimit).Named(Html.Resource("PPP_Resources, PPP_View_Pool_Index_Addresses"));
               column.For(c => String.Format("{0:F}", c.Cost)).Named(Html.Resource("PPP_Resources, PPP_View_Pool_Index_NextPool"));

           }).Attributes(id => "grid").Render();
        %>
    </div>
    <p>
        <%= Html.ActionLink(Html.Resource("PPP_Resources, PPP_View_Profile_Index_CreateNew"), "Create") %>
        <%--|
        <%= Html.ActionLink(Html.Resource("PPP_Resources, PPP_View_Profile_Index_DeleteProfiles"), "DeleteUnused")%>--%>
    </p>
</asp:Content>
