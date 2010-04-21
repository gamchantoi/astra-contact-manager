<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ContactManager.Models.CustomResource>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <% using (Html.BeginForm())
       {%>
    <fieldset>
        <legend>Edit Contarct</legend>
        <p>
            <%= Html.LabelFor(model => model.Key) %>
            <%= Html.TextBoxFor(model => model.Key) %>
            <%= Html.ValidationMessageFor(model => model.Key) %>
        </p>
        <p>
            <%= Html.LabelFor(model => model.Value) %>
            <%= Html.TextBoxFor(model => model.Value) %>
            <%= Html.ValidationMessageFor(model => model.Value) %>
        </p>
        <p>
            <%= Html.LabelFor(model => model.LastActivityDate) %>
            <%= Html.Label(Model.LastActivityDate.ToString())%>
        </p>
        <br />
        <div class="reditor" id="reditor"></div>
        <br />
        <p>
            <input type="submit" value="Save" />
        </p>
    </fieldset>
    <% } %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
    $(document).ready(function() {
        $('#reditor').reditor({
        basepath: '/',
            iconpath: 'css/img/ico/',
            width: '700px',
            height: '300px'
        });
    });
    </script>
    
</asp:Content>
