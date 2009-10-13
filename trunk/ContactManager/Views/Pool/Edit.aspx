<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ContactManager.Models.Pool>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%= Html.ValidationSummary("Edit was unsuccessful. Please correct the errors and try again.") %>

    <% using (Html.BeginForm()) {%>
    <%= Html.Hidden("PoolId", Model.PoolId)%>
    <%= Html.Hidden("Name", Model.Name)%>
        <fieldset class="fields">
            <legend>Edit Pool: <%=Html.Encode(Model.Name)%></legend>
            <p>
                <label for="Addresses">Addresses:</label>
                <%= Html.TextBox("Addresses", Model.Addresses) %>
                <%= Html.ValidationMessage("Addresses", "*") %>
            </p>
            <p>
                <label for="NextPool">NextPool:</label>
                <%= Html.DropDownList("NextPool", (SelectList)ViewData["Pools"])%>
                <%= Html.ValidationMessage("NextPool", "*")%>
            </p>
            <p>
                <input type="submit" value="Save" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%=Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>

