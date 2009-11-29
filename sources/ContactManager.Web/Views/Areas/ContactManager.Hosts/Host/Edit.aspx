<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ContactManager.Models.Host>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%= Html.ValidationSummary("Edit was unsuccessful. Please correct the errors and try again.") %>

    <% using (Html.BeginForm()) {%>

        <fieldset>
            <legend>Edit Host: <%=Html.Encode(Model.Address)%></legend>
            <%= Html.Hidden("HostId", Model.HostId) %>
            <p>
                <label for="Address">Address:</label>
                <%= Html.TextBox("Address", Model.Address) %>
                <%= Html.ValidationMessage("Address", "*") %>
            </p>
            <p>
                <label for="UserName">UserName:</label>
                <%= Html.TextBox("UserName", Model.UserName) %>
                <%= Html.ValidationMessage("UserName", "*") %>
            </p>
            <p>
                <label for="UserPassword">UserPassword:</label>
                <%= Html.Password("UserPassword", Model.UserPassword) %>
                <%= Html.ValidationMessage("UserPassword", "*") %>
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

