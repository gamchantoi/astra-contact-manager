<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ContactManager.Messages.ViewModels.MessageViewModel>" %>

<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="ContactManager.Web.Helpers" %>
<%@ Import Namespace="MvcContrib" %>
<%@ Import Namespace="MvcContrib.UI.Grid.ActionSyntax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="container">
        <% Html.Grid(Model.Messagess)
           .Columns(column =>
           {
               column.For(
                   t =>
                   Html.ActionLink(Html.Resource("Messages_Resources, Messages_View_Index_Details"), "Details", new { id = t.MessageId }) + " | " + Html.ActionLink(Html.Resource("Messages_Resources, Messages_View_Index_Delete"), "Delete", new { id = t.MessageId })).DoNotEncode();
               column.For(t => t.Title).Named(Html.Resource("Messages_Resources, Messages_View_Index_Title"));
               column.For(t => t.Text).Named(Html.Resource("Messages_Resources, Messages_View_Index_Text"));
               column.For(t => t.Date).Named(Html.Resource("Accounts_Resources, Accounts_View_Index_Date"));

           }).Attributes(id => "grid").Render();
        %>
        <p>
            <%= Html.JSLink(Html.Resource("Messages_Resources, Messages_View_Index_CreateNew"), "ShowMessageDialog", Url.Content("~/Messages/Message/Create"))%>&nbsp;|&nbsp;
            <%= Html.ActionLink(Html.Resource("Messages_Resources, Messages_View_Index_CreateMessageType"), "Index", "MessageType")%>
        </p>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">
        $(document).ready(function() {
            if ('<%=Model.Messagess.Count() %>' > 0) {
                jQuery('#grid').dataTable({
                    "iDisplayLength": 10,
                    "aaSorting": [[1, "asc"]],
                    "aoColumns": [{ "bSortable": false }, null, null, null]
                });
            }
            $("#grid thead").addClass("ui-widget-header");
        });
    </script>

</asp:Content>
