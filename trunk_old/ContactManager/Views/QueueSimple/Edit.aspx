<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ContactManager.Models.QueueSimple>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%= Html.ValidationSummary("Edit was unsuccessful. Please correct the errors and try again.") %>

    <% using (Html.BeginForm()) {%>
    <%= Html.Hidden("QueueId", Model.QueueId)%>
        <fieldset class="fields">
            <legend>Edit Queue: <%=Html.Encode(Model.QueueName)%></legend>
            <p>
                <label for="QueueName">QueueName:</label>
                <%= Html.TextBox("QueueName", Model.QueueName) %>
                <%= Html.ValidationMessage("QueueName", "*") %>
            </p>
            <p>
                <label for="Comment">Comment:</label>
                <%= Html.TextBox("Comment", Model.Comment) %>
                <%= Html.ValidationMessage("Comment", "*") %>
            </p>
            <p>
                <label for="TariffId">TariffId:</label>
                <%= Html.DropDownList("TariffId", (SelectList)ViewData["Tariffs"])%>
                <%= Html.ValidationMessage("TariffId", "*")%>
            </p>
            <p>
                <label for="TargetAddress">TargetAddress:</label>
                <% foreach (var user in Model.astra_Clients)
                { %>
                    <a href='<%=Url.Action("Edit", "User", new {id=user.UserId}) %>'><%= Html.Encode(user.RemoteAddress)%></a>
                    <a href='<%=Url.Action("Remove", new {id=user.UserId}) %>'><img src="/Content/Delete.png" alt="Delete" /></a>                
                <%} %>
                <%= Html.TextBox("TargetAddress", Model.TargetAddress) %>
                <%= Html.ValidationMessage("TargetAddress", "*") %>                
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

