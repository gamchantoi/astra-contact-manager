<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ContactManager.Models.Pool>" %>
<%@ Import Namespace="ContactManager.Web.Helpers"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%= Html.ValidationSummary(Html.Resource("PPP_Resources, PPP_View_Pool_Edit_ValidationSummary"))%>

    <% using (Html.BeginForm()) {%>
    <%= Html.Hidden("PoolId", Model.PoolId)%>
    <%= Html.Hidden("Name", Model.Name)%>
        <fieldset class="fields">
            <legend><%= Html.Resource("PPP_Resources, PPP_View_Pool_Edit_EditPool")%><%=Html.Encode(Model.Name)%></legend>
            <p>
                <label for="Addresses"><%= Html.Resource("PPP_Resources, PPP_View_Pool_Edit_Addresses")%></label>
                <%= Html.TextBox("Addresses", Model.Addresses) %>
                <%= Html.ValidationMessage("Addresses", "*") %>
            </p>
            <p>
                <label for="NextPool"><%= Html.Resource("PPP_Resources, PPP_View_Pool_Edit_NextPool")%></label>
                <%= Html.DropDownList("NextPool", (SelectList)ViewData["Pools"])%>
                <%= Html.ValidationMessage("NextPool", "*")%>
            </p>
            <p>
                <input type="submit" value=<%= Html.Resource("PPP_Resources, PPP_View_Pool_Edit_Save")%> />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%=Html.ActionLink(Html.Resource("PPP_Resources, PPP_View_Pool_Edit_BackToList"), "Index")%>
    </div>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>

