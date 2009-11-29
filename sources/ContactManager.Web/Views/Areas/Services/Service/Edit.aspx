<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ContactManager.Models.Service>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%= Html.ValidationSummary("Edit was unsuccessful. Please correct the errors and try again.") %>
    <% using (Html.BeginForm())
       {%>
    <%= Html.Hidden("ServiceId", Model.ServiceId)%>
    <fieldset>
        <legend>Edit Service:
            <%=Html.Encode(Model.Name)%></legend>
        <%if (string.IsNullOrEmpty(Model.SystemData))
          { %>
        <p>
            <label for="Name">
                Name:</label>
            <%= Html.TextBox("Name", Model.Name)%>
            <%= Html.ValidationMessage("Name", "*")%>
        </p>
        <%} %>
        <%else
            { %>
            <%= Html.Hidden("Name", Model.Name)%>
        <%} %>
        <p>
            <label for="Comment">
                Comment:</label>
            <%= Html.TextBox("Comment", Model.Comment) %>
            <%= Html.ValidationMessage("Comment", "*") %>
        </p>
        <p>
            <label for="Cost">
                Cost:</label>
            <%= Html.TextBox("Cost", String.Format("{0:F}", Model.Cost)) %>
            <%= Html.ValidationMessage("Cost", "*") %>
        </p>
        <p>
            <label for="Visible">
                Visible:</label>
            <%= Html.CheckBox("Visible", Model.Visible)%>
            <%= Html.ValidationMessage("Visible", "*") %>
        </p>
        <p>
            <label for="IsRegular">
                IsRegular:</label>
            <%= Html.CheckBox("IsRegular", Model.IsRegular)%>
            <%= Html.ValidationMessage("IsRegular", "*") %>
        </p>
        <p>
            <label for="Active">
                Active:</label>
            <%= Html.CheckBox("Active", Model.Active)%>
            <%= Html.ValidationMessage("Active", "*")%>
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
