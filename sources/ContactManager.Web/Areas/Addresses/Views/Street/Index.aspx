<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<ContactManager.Models.Street>>" %>
<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="ContactManager.Web.Helpers" %>


    <script language="javascript" type="text/javascript">
        $(document).ready(function() {
            if ('<%=Model.Count() %>' > 0) {
                jQuery('#grid_Streets').dataTable({
                    "iDisplayLength": 10,
                    "aaSorting": [[1, "asc"]],
                    "aoColumns": [null, null]
                });
            }
            $("#grid thead").addClass("ui-widget-header");
        });
    </script>
    
 <div id="container">
        <% Html.Grid(Model)
           .Columns(column =>
           {
               column.For(c => c.Name + Html.JSIconLink("Edit", "ShowDialog", Url.Content("~/Addresses/Street/Edit/") + c.Name, "ui-icon-wrench")).DoNotEncode()
                   .Named(Html.Resource("Addresses_Resources, Addresses_Street_Index_Name"));
               column.For(c => c.Tag).Named(Html.Resource("Addresses_Resources, Addresses_Street_Index_Tag"));
           }).Attributes(id => "grid_Streets").Render();
        %>
    </div>
    
    
   