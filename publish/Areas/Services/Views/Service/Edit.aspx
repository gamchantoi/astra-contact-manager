<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ContactManager.Models.Service>" %>

<%@ Import Namespace="ContactManager.Web.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script>
        $(document).ready(function() {
            $("#createForm").validate();
        });
    </script>

    <%= Html.ValidationSummary("Edit was unsuccessful. Please correct the errors and try again.") %>
    <% using (Html.BeginForm("Edit", "Service", FormMethod.Post, new { id = "createForm" }))
       {%>
    <%= Html.Hidden("ServiceId", Model.ServiceId)%>
    <fieldset>
        <legend>
            <%= Html.Resource("Services_Resources, Services_View_Edit_EditService")%>:
            <%=Html.Encode(Model.Name)%></legend>
        <%if (string.IsNullOrEmpty(Model.SystemData))
          { %>
        <p>
            <label for="Name">
                <%= Html.Resource("Services_Resources, Services_View_Create_Name")%>:</label>
            <%= Html.TextBox("Name", Model.Name, new { @class = "required"})%>
            <%= Html.ValidationMessage("Name", "*")%>
        </p>
        <%} %>
        <%else
            { %>
        <%= Html.Hidden("Name", Model.Name)%>
        <%} %>
        <p>
            <label for="Comment">
                <%= Html.Resource("Services_Resources, Services_View_Create_Comment")%>:</label>
            <%= Html.TextBox("Comment", Model.Comment, new { @class = "required"}) %>
            <%= Html.ValidationMessage("Comment", "*") %>
        </p>
        <p>
            <label for="Cost">
                <%= Html.Resource("Services_Resources, Services_View_Create_Cost")%>:</label>
            <%= Html.TextBox("Cost", String.Format("{0:F}", Model.Cost), new { @class = "required" })%>
            <%= Html.ValidationMessage("Cost", "*") %>
        </p>
        <p>
            <label for="Visible">
                <%= Html.Resource("Services_Resources, Services_View_Create_Visible")%>:</label>
            <%= Html.CheckBox("Visible", Model.Visible)%>
            <%= Html.ValidationMessage("Visible", "*") %>
        </p>
        <p>
            <label for="IsRegular">
                <%= Html.Resource("Services_Resources, Services_View_Create_IsRegular")%>:</label>
            <%= Html.CheckBox("IsRegular", Model.IsRegular)%>
            <%= Html.ValidationMessage("IsRegular", "*") %>
        </p>
        <p>
            <label for="Active">
                <%= Html.Resource("Services_Resources, Services_View_Create_Active")%>:</label>
            <%= Html.CheckBox("Active", Model.Active)%>
            <%= Html.ValidationMessage("Active", "*")%>
        </p>
        <p>
            <input type="submit" value='<%= Html.Resource("Services_Resources, Services_View_Edit_Save")%>' />
        </p>
    </fieldset>
    <% } %>
    <div>
        <%=Html.ActionLink(Html.Resource("Services_Resources, Services_View_Create_BackToList"), "Index")%>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
