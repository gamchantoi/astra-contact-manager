<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<ContactManager.Models.Address>" %>

<%@ Import Namespace="ContactManager.Web.Helpers" %>

<script>
    $(document).ready(function() {
        $("#createForm").validate();
    });
</script>

<% using (Html.BeginForm("Create", "Address", FormMethod.Post, new { id = "createForm" }))
   {%>
<%= Html.Hidden("UserId", ViewData["UserId"])%>
<fieldset class="fields">
    <legend>
        <%= Html.Resource("Addresses_Resources, Addresses_View_Create_CreateAddress")%></legend>
    <p>
        <label for="City">
            <%= Html.Resource("Addresses_Resources, Addresses_View_Create_City")%></label>
        <%= Html.TextBox("City", "", new { @class = "required" })%>
        <%= Html.ValidationMessage("City", "*") %>
    </p>
    <%--    <p>
        <label for="Street.StreetId">
            <%= Html.Resource("Addresses_Resources, Addresses_View_Create_Street")%></label>
        <%= Html.DropDownList("Street.StreetId", (SelectList)ViewData["Streets"])%>
        <%= Html.ValidationMessage("Street.StreetId", "*")%>
        <%= Html.ActionLink(Html.Resource("Addresses_Resources, Addresses_View_Create_AddNew"), "Create", "Street")%>
        <%= Html.JSLink("Create", "ShowDialog", Url.Content("~/Addresses/Street/Create/"))%>
    </p>--%>
    <p>
        <label for="Street.StreetId">
            <%= Html.Resource("Addresses_Resources, Addresses_View_Edit_Street")%></label>
        <%= Html.DropDownList("Street.StreetId", (SelectList)ViewData["Streets"], new { onchange = "ShowStreetDialog(this.value , '" + Url.Content("~/Addresses/Street/Create") + "'); " } )%>
        <%= Html.ValidationMessage("Street.StreetId", "*")%>
    </p>
    <label for="Building">
        <%= Html.Resource("Addresses_Resources, Addresses_View_Create_Building")%></label>
    <%= Html.TextBox("Building","", new { @class = "required" })%>
    <%= Html.ValidationMessage("Building", "*") %>
    </p>
    <p>
        <label for="Room">
            <%= Html.Resource("Addresses_Resources, Addresses_View_Create_Room")%></label>
        <%= Html.TextBox("Room", "", new { @class = "required" })%>
        <%= Html.ValidationMessage("Room", "*") %>
    </p>
    <p>
        <label for="Details">
            <%= Html.Resource("Addresses_Resources, Addresses_View_Create_Details")%></label>
        <%= Html.TextBox("Details", "", new { @class = "required" })%>
        <%= Html.ValidationMessage("Details", "*") %>
    </p>
    <p>
        <input type="submit" value='<%= Html.Resource("Addresses_Resources, Addresses_View_Create_Create")%>' />
    </p>
</fieldset>
<% } %>
