<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ContactManager.Models.Host>" %>
<%@ Import Namespace="ContactManager.Web.Helpers"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%= Html.ValidationSummary( Html.Resource("Hosts_Resources, Hosts_View_Edit_ValidationSummary")) %>

    <% using (Html.BeginForm()) {%>

        <fieldset>
            <legend><%=  Html.Resource("Hosts_Resources, Hosts_View_Edit_EditHost")%><%=Html.Encode(Model.Address)%></legend>
            <%= Html.Hidden("HostId", Model.HostId) %>
            <p>
                <label for="Address"><%= Html.Resource("Hosts_Resources, Hosts_View_Edit_Address")%></label>
                <%= Html.TextBox("Address", Model.Address) %>
                <%= Html.ValidationMessage("Address", "*") %>
            </p>
            <p>
                <label for="UserName"><%= Html.Resource("Hosts_Resources, Hosts_View_Edit_UserName")%></label>
                <%= Html.TextBox("UserName", Model.UserName) %>
                <%= Html.ValidationMessage("UserName", "*") %>
            </p>
            <p>
                <label for="UserPassword"><%= Html.Resource("Hosts_Resources, Hosts_View_Edit_UserPassword")%></label>
                <%= Html.Password("UserPassword", Model.UserPassword) %>
                <%= Html.ValidationMessage("UserPassword", "*") %>
            </p>
            <p>
                <input type="submit" value=<%= Html.Resource("Hosts_Resources, Hosts_View_Edit_Save")%> />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%= Html.ActionLink(Html.Resource("Hosts_Resources, Hosts_View_Edit_BackToList"), "Index")%>
    </div>

</asp:Content>

