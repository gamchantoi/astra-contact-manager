<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ContactManager.Models.Address>" %>
<%@ Import Namespace="ContactManager.Web.Helpers"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <%= Html.ValidationSummary(Html.Resource("Addresses_Resources, Addresses_View_Create_ValidationSummary")) %>
    <% using (Html.BeginForm("Create", "Address"))
       {%>
    <fieldset class="fields">
        <legend><%= Html.Resource("Addresses_Resources, Addresses_View_Create_CreateAddress")%></legend>
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
            <label for="Details">
                <%= Html.Resource("Addresses_Resources, Addresses_View_Create_Details")%></label>
            <%= Html.TextBox("Details") %>
            <%= Html.ValidationMessage("Details", "*") %>
        </p>
        <p>
            <label for="Room">
                <%= Html.Resource("Addresses_Resources, Addresses_View_Create_Room")%></label>
            <%= Html.TextBox("Room") %>
            <%= Html.ValidationMessage("Room", "*") %>
        </p>
        <p>
            <input type="submit" value=<%= Html.Resource("Addresses_Resources, Addresses_View_Create_Create")%> />
        </p>
    </fieldset>
    <% } %>
    <div>
        <%=Html.ActionLink(Html.Resource("Addresses_Resources, Addresses_View_Create_BackToList"), "Index") %>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
