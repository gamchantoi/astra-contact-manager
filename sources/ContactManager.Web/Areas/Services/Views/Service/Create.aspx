<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ContactManager.Models.Service>" %>

<%@ Import Namespace="ContactManager.Web.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%= Html.ValidationSummary(Html.Resource("Services_Resources, Services_View_Create_ValidationSummary"))%>

    <script>
        $(document).ready(function() {
            $("#createForm").validate();
        });
    </script>

    <% using (Html.BeginForm("Create", "Service", FormMethod.Post, new { id = "createForm" }))
       {%>
    <fieldset>
        <legend>
            <%= Html.Resource("Services_Resources, Services_View_Create_CreateNewService")%></legend>
        <p>
            <label for="Name">
                <%= Html.Resource("Services_Resources, Services_View_Create_Name")%>:</label>
            <%= Html.TextBox("Name", "", new { @class = "required" })%>
            <%= Html.ValidationMessage("Name", "*") %>
        </p>
        <p>
            <label for="Comment">
                <%= Html.Resource("Services_Resources, Services_View_Create_Comment")%>:</label>
            <%= Html.TextBox("Comment", "", new { @class = "required" })%>
            <%= Html.ValidationMessage("Comment", "*")%>
        </p>
        <p>
            <label for="Cost">
                <%= Html.Resource("Services_Resources, Services_View_Create_Cost")%>:</label>
            <%= Html.TextBox("Cost", "", new { @class = "required" })%>
            <%= Html.ValidationMessage("Cost", "*") %>
        </p>
        <p>
            <label for="IsRegular">
                <%= Html.Resource("Services_Resources, Services_View_Create_IsRegular")%>:</label>
            <%= Html.CheckBox("IsRegular") %>
            <%= Html.ValidationMessage("IsRegular", "*") %>
        </p>
        <p>
            <label for="Active">
                <%= Html.Resource("Services_Resources, Services_View_Create_Active")%>:</label>
            <%= Html.CheckBox("Active", true)%>
            <%= Html.ValidationMessage("Active", "*")%>
        </p>
        <p>
            <label for="Visible">
                <%= Html.Resource("Services_Resources, Services_View_Create_Visible")%>:</label>
            <%= Html.CheckBox("Visible", true)%>
            <%= Html.ValidationMessage("Visible", "*")%>
        </p>
        <p>
            <input type="submit" value='<%= Html.Resource("Services_Resources, Services_View_Create_Create")%>' />
        </p>
    </fieldset>
    <% } %>
    <div>
        <%=Html.ActionLink(Html.Resource("Services_Resources, Services_View_Create_BackToList"), "Index") %>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
