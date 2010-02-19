<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<ContactManager.Models.Address>>" %>

<%@ Import Namespace="ContactManager.Web.Helpers" %>
<%@ Import Namespace="MvcContrib" %>
<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="MvcContrib.UI.Grid.ActionSyntax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="container">
        <% Html.Grid(Model)
           .Columns(column =>
           {
               column.For(c => c.City + Html.JSIconLink("Edit", "ShowDialog", Url.Content("~/Addresses/Address/EditForAddress/") + c.AddressId, "ui-icon-wrench")).DoNotEncode()
                   .Named(Html.Resource("Addresses_Resources, Addresses_View_Index_City"));
               column.For(c => c.Building).Named(Html.Resource("Addresses_Resources, Addresses_View_Index_Building"));
               column.For(c => c.Details).Named(Html.Resource("Addresses_Resources, Addresses_View_Index_Details"));
               column.For(c => c.Room).Named(Html.Resource("Addresses_Resources, Addresses_View_Index_Room"));

           }).Attributes(id => "grid").Render();
        %>
    </div>
    <p>
        <%= Html.JSLink(Html.Resource("Addresses_Resources, Addresses_View_Index_CreateNewStreet"), "ShowStreetDialog", new[]{"0", Url.Content("~/Addresses/Street/Create")})%>
            |
        <%= Html.JSLink(Html.Resource("Addresses_Resources, Addresses_View_Index_ListStreet"), "ShowDialog", Url.Content("~/Addresses/Street/Index/"))%>
        </p>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">

    <script src="<%= Url.Content("~/Scripts/astra-streets.js")%>" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        $(document).ready(function() {
            if ('<%=Model.Count() %>' > 0) {
                jQuery('#grid').dataTable({
                    "iDisplayLength": 10,
                    "aaSorting": [[1, "asc"]],
                    "aoColumns": [null, null, null, null]
                });
            }
            $("#grid thead").addClass("ui-widget-header");
        });
    </script>

</asp:Content>
