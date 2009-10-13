<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ContactManager.Models.QueueSimple>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%= Html.ValidationSummary("Create was unsuccessful. Please correct the errors and try again.") %>
    <% using (Html.BeginForm()) {%>
        <fieldset class="fields">
            <legend>Create New Queue</legend>
            <p>
                <label for="QueueName">QueueName:</label>
                <%= Html.TextBox("QueueName") %>
                <%= Html.ValidationMessage("QueueName", "*") %>
            </p>
            <p>
                <label for="Comment">Comment:</label>
                <%= Html.TextBox("Comment") %>
                <%= Html.ValidationMessage("Comment", "*") %>
            </p>
            <p>
                <label for="TariffId">TariffId:</label>
                <%= Html.DropDownList("TariffId", (SelectList)ViewData["Tariffs"])%>
                <%= Html.ValidationMessage("TariffId", "*")%>
            </p>
            <p>
                <label for="TargetAddress">TargetAddress:</label>
                <%= Html.TextBox("TargetAddress") %>
                <%= Html.ValidationMessage("TargetAddress", "*") %>
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

