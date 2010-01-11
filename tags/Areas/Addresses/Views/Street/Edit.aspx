<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ContactManager.Models.Street>" %>
<%@ Import Namespace="ContactManager.Web.Helpers"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
    </h2>
    <% using (Html.BeginForm())
       {%>
    <fieldset>
        <legend><%= Html.Resource("Addresses_Resources, Addresses_Street_Edit_EditStreet")%></legend>
        <%--            <p>
                <%= Html.LabelFor(model => model.StreetId) %>
                <%= Html.TextBoxFor(model => model.StreetId) %>
                <%= Html.ValidationMessageFor(model => model.StreetId) %>
            </p>--%>
        <%= Html.Hidden("StreetId", Model.StreetId) %>
        <p>
            <%= Html.Label(Html.Resource("Addresses_Resources, Addresses_Street_Create_Name"))%>
            <%= Html.TextBoxFor(model => model.Name) %>
            <%= Html.ValidationMessageFor(model => model.Name) %>
        </p>
        <p>
            <%= Html.Label(Html.Resource("Addresses_Resources, Addresses_Street_Create_Tag")) %>
            <%= Html.TextBoxFor(model => model.Tag) %>
            <%= Html.ValidationMessageFor(model => model.Tag) %>
        </p>
        <p>
            <input type="submit" value=<%= Html.Resource("Addresses_Resources, Addresses_Street_Edit_Save") %> />
        </p>
    </fieldset>
    <% } %>
    <div>
        <%=Html.ActionLink(Html.Resource("Addresses_Resources, Addresses_Street_Create_BackToList"), "Index") %>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
