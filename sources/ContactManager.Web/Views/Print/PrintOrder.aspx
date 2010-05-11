<%@ Page Title="" Language="C#"  Inherits="System.Web.Mvc.ViewPage<ContactManager.Models.CustomResource>" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">--%>
    <fieldset>
<%--        <legend>Order</legend>--%>
        <%--        <div class="display-label">Key</div> MasterPageFile="~/Views/Shared/Site.Master"
        <div class="display-field"><%= Html.Encode(Model.Key) %></div>--%>
        <%--        <div class="display-label">Value</div>
        <div class="display-field"><%= Html.Encode(Model.Value) %></div>--%>
        <div>
            <%= Model.Value%>
        </div>
    </fieldset>
<%--    <p>
        <%= Html.ActionLink("Edit", "Edit", new { id=Model.Key }) %>
        |
        <%= Html.ActionLink("Back to List", "Index") %>
    </p>--%>
<%--</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>--%>
