<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ContactManager.Models.Profile>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%= Html.ValidationSummary("Create was unsuccessful. Please correct the errors and try again.") %>

    <% using (Html.BeginForm()) {%>
        <fieldset class="fields">
            <legend>Create New Profile</legend>
            <p>
                <label for="Name">Name:</label>
                <%= Html.TextBox("Name") %>
                <%= Html.ValidationMessage("Name", "*") %>
            </p>
            <p>
                <label for="LocalAddress">LocalAddress:</label>
                <%= Html.TextBox("LocalAddress") %>
                <%= Html.ValidationMessage("LocalAddress", "*") %>
            </p>
            <p>
                <label for="PoolId">Pool:</label>
                <%= Html.DropDownList("PoolId", (SelectList)ViewData["Pools"])%>
                <%= Html.ValidationMessage("PoolId", "*")%>
            </p>
            <p>
                <label for="RateLimit">RateLimit:</label>
                <%= Html.TextBox("RateLimit") %>
                <%= Html.ValidationMessage("RateLimit", "*") %>
            </p>
            <p>
                <label for="Cost">Cost:</label>
                <%= Html.TextBox("Cost") %>
                <%= Html.ValidationMessage("Cost", "*") %>
            </p>
            <p>
                <label for="Comment">Comment:</label>
                <%= Html.TextBox("Comment")%>
                <%= Html.ValidationMessage("Comment", "*")%>
            </p>
            <p>
                <input type="submit" value="Create" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%=Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>

