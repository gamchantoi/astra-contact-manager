<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<ContactManager.Models.Address>" %>

<%@ Import Namespace="ContactManager.Web.Helpers" %>
<% using (Html.BeginForm("Create", "Address"))
       {%>
<%= Html.Hidden("UserId", ViewData["UserId"])%>
<fieldset class="fields">
    <legend>
        <%= Html.Resource("Addresses_Resources, Addresses_View_Create_CreateAddress")%></legend>
    <p>
        <label for="City">
            <%= Html.Resource("Addresses_Resources, Addresses_View_Create_City")%></label>
        <%= Html.TextBox("City") %>
        <%= Html.ValidationMessage("City", "*") %>
    </p>
    <p>
        <label for="Street.StreetId">
            <%= Html.Resource("Addresses_Resources, Addresses_View_Create_Street")%></label>
        <%= Html.DropDownList("Street.StreetId", (SelectList)ViewData["Streets"])%>
        <%= Html.ValidationMessage("Street.StreetId", "*")%>
        <%= Html.ActionLink(Html.Resource("Addresses_Resources, Addresses_View_Create_AddNew"), "Create", "Street")%>
    </p>
    <p>
        <label for="Building">
            <%= Html.Resource("Addresses_Resources, Addresses_View_Create_Building")%></label>
        <%= Html.TextBox("Building") %>
        <%= Html.ValidationMessage("Building", "*") %>
    </p>
    <p>
        <label for="Room">
            <%= Html.Resource("Addresses_Resources, Addresses_View_Create_Room")%></label>
        <%= Html.TextBox("Room") %>
        <%= Html.ValidationMessage("Room", "*") %>
    </p>
    <p>
        <label for="Details">
            <%= Html.Resource("Addresses_Resources, Addresses_View_Create_Details")%></label>
        <%= Html.TextBox("Details") %>
        <%= Html.ValidationMessage("Details", "*") %>
    </p>
    <p>
        <input type="submit" value='<%= Html.Resource("Addresses_Resources, Addresses_View_Create_Create")%>' />
    </p>
</fieldset>
<% } %>
