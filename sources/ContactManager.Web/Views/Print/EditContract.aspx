<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ContactManager.Models.CustomResource>" %>

<%@ Import Namespace="ContactManager.Web.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset>
        <legend>
            <%= Html.Resource("Accounts_Resources, Accounts_View_Index_EditContract")%></legend>
        <p>
            <%= Html.Hidden("Key") %>
        </p>
        <div class="reditor" id="reditor">
            <%= Model.Value%>
        </div>
    </fieldset>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        $(document).ready(function() {
            $('#reditor').reditor({
                basepath: '../media/reditor/',
                iconpath: 'css/img/ico/',
                width: '700px',
                height: '300px'
            });
        });
    </script>

</asp:Content>
