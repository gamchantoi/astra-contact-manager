<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<ContactManager.Models.Address>" %>

<%@ Import Namespace="ContactManager.Web.Helpers" %>
<% using (Html.BeginForm())
   {%>
<%= Html.HiddenFor(model => model.AddressId)%>
<fieldset>
    <legend>
        <%= Html.Resource("Addresses_Resources, Addresses_View_Edit_EditAddress") %></legend>
    <p>
        <%= Html.Label(Html.Resource("Addresses_Resources, Addresses_View_Edit_City"))%>
        <%= Html.TextBoxFor(model => model.City) %>
        <%= Html.ValidationMessageFor(model => model.City) %>
    </p>
    <p>
        <label for="Street.StreetId">
            <%= Html.Resource("Addresses_Resources, Addresses_View_Edit_Street")%></label>
        <%= Html.DropDownList("Street.StreetId", (SelectList)ViewData["Streets"], new { onchange = "ShowStreetDialog(this.value , '" + Url.Content("~/Addresses/Street/Create") + "'); " })%>
        <%= Html.ValidationMessage("Street.StreetId", "*")%>
    </p>
    <p>
        <%= Html.Label(Html.Resource("Addresses_Resources, Addresses_View_Edit_Building"))%>
        <%= Html.TextBoxFor(model => model.Building) %>
        <%= Html.ValidationMessageFor(model => model.Building) %>
    </p>
    <p>
        <%= Html.Label(Html.Resource("Addresses_Resources, Addresses_View_Edit_Details"))%>
        <%= Html.TextBoxFor(model => model.Details) %>
        <%= Html.ValidationMessageFor(model => model.Details) %>
    </p>
    <p>
        <%= Html.Label(Html.Resource("Addresses_Resources, Addresses_View_Edit_Room"))%>
        <%= Html.TextBoxFor(model => model.Room) %>
        <%= Html.ValidationMessageFor(model => model.Room) %>
    </p>
    <p>
        <input type="submit" value="<%= Html.Resource("Addresses_Resources, Addresses_View_Edit_Save") %>" />
    </p>
</fieldset>
<% } %>