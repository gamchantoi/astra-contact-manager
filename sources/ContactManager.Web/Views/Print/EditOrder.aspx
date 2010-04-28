<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ContactManager.Models.CustomResource>" %>
<%@ Import Namespace="ContactManager.Web.Helpers"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <% using (Html.BeginForm())
       {%>
    <fieldset>
        <legend>Edit Contarct</legend>
        <p><%= Html.Hidden("Key") %></p>
        <p>
            <%= Html.LabelFor(model => model.Value) %>
            <%= Html.TextBoxFor(model => model.Value) %>
            <%= Html.ValidationMessageFor(model => model.Value) %>
        </p>
        <p>
            <input type="submit" value="Save" />
        </p>
    </fieldset>
    <% } %>
    <div class="reditor" id="reditor">
    </div>
    <div>
    <p> </p>
    <p> </p>
    <p> </p>
    <p> </p>
    </div>
    <p>
<%--        <input onclick="SubmitCustomResource('<%= Url.Content("~/Web/Settings/EditContract")%>');"
            type="button" value="<%= "Submit"%>" />--%>
    </p>
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
