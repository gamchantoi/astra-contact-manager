<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ContactManager.Models.Address>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Edit</h2>
    <% using (Html.BeginForm())
       {%>
    <fieldset>
        <legend>Fields</legend>
        <p>
            <%= Html.Hidden("AddressId", Model.AddressId)%>
<%--            <%= Html.LabelFor(model => model.AddressId) %>
            <%= Html.TextBoxFor(model => model.AddressId) %>
            <%= Html.ValidationMessageFor(model => model.AddressId) %>--%>
        </p>
        <p>
            <label for="Street.StreetId">
                Street:</label>
            <%= Html.DropDownList("Street.StreetId", (SelectList)ViewData["Streets"])%>
            <%= Html.ValidationMessage("Street.StreetId", "*")%>
            <%= Html.ActionLink("Add New", "CreateStreet")%>
        </p>
        <p>
            <%= Html.LabelFor(model => model.City) %>
            <%= Html.TextBoxFor(model => model.City) %>
            <%= Html.ValidationMessageFor(model => model.City) %>
        </p>
        <p>
            <%= Html.LabelFor(model => model.Building) %>
            <%= Html.TextBoxFor(model => model.Building) %>
            <%= Html.ValidationMessageFor(model => model.Building) %>
        </p>
        <p>
            <%= Html.LabelFor(model => model.Details) %>
            <%= Html.TextBoxFor(model => model.Details) %>
            <%= Html.ValidationMessageFor(model => model.Details) %>
        </p>
        <p>
            <%= Html.LabelFor(model => model.Room) %>
            <%= Html.TextBoxFor(model => model.Room) %>
            <%= Html.ValidationMessageFor(model => model.Room) %>
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
